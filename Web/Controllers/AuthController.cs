using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Data.Dto;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using OAuth2.Client;
using OAuth2.Client.Impl;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Web.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("google")]
        public async Task<HttpResponseMessage> PostGoogle(HttpRequestMessage request)
        {
            string json = await request.Content.ReadAsStringAsync();
            var auth = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(json, new {Code = "", ClientId = "", RedirectUri = ""});

            var client = new GoogleClient(new RequestFactory(), new RuntimeClientConfiguration
            {
                ClientId = auth.ClientId,
                ClientSecret = System.Configuration.ConfigurationManager.AppSettings["GoogleSecret"],
                RedirectUri = auth.RedirectUri
            });

            User user;
            try
            {
                var input = new NameValueCollection();
                input.Add("Code", auth.Code);
                var userInfo = client.GetUserInfo(input);
                user = new User
                {
                    Id = userInfo.Email,
                    ExternalId = userInfo.Id,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    ProviderName = userInfo.ProviderName
                };
            }
            catch (Exception ex)
            {
                throw;
                //Log.Error().Exception(ex).Critical().Message("External login failed: {0}", ex.Message).Tag("External Login", client.Name).Property("Auth Info", authInfo).ContextProperty("HttpActionContext", ActionContext).Write();
                //return BadRequest("Unable to get user info.");
            }

            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoUri"]);
            var database = mongoClient.GetDatabase("MongoLab-c");
            var userCollection = database.GetCollection<User>("food");

            var filter = Builders<User>.Filter.Where(x => x.Id == user.Id);
            var existingUser = await userCollection.Find(filter).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                await userCollection.InsertOneAsync(user);
            }

            var token = GetNewToken();
            return request.CreateResponse(HttpStatusCode.OK, new
            {
                Token = new {
                    Id = token
                }
            });
        }

        public static string GetNewToken()
        {
            return GetRandomString(40);
        }

        public static string GetRandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "length cannot be less than zero.");

            if (string.IsNullOrEmpty(allowedChars))
                throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length)
                throw new ArgumentException($"allowedChars may contain no more than {byteSize} characters.");

            using (var rng = new RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];

                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i])
                            continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }

                return result.ToString();
            }
        }


    }

    public class User
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProviderName { get; set; }
    }
}