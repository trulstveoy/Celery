using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Data.Dto;
using MongoDB.Driver;
using Newtonsoft.Json;
using OAuth2.Client.Impl;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
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
            var auth = JsonConvert.DeserializeAnonymousType(json, new {Code = "", ClientId = "", RedirectUri = ""});

            var client = new GoogleClient(new RequestFactory(), new RuntimeClientConfiguration
            {
                ClientId = auth.ClientId,
                ClientSecret = ConfigurationManager.AppSettings["GoogleSecret"],
                RedirectUri = auth.RedirectUri
            });

            var token = GetNewToken();
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
                    ProviderName = userInfo.ProviderName,
                    BearerToken = token
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
            var userCollection = database.GetCollection<User>("user");
            await userCollection.ReplaceOneAsync(item => item.Id == user.Id, user, new UpdateOptions {IsUpsert = true});
           
            return request.CreateResponse(HttpStatusCode.OK, new
            {
                Token = token
            });
        }

        [HttpGet]
        [Route("me")]
        public async Task<HttpResponseMessage> GetMe(HttpRequestMessage request)
        {
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues("Authorization", out headerValues))
            {
                var token = headerValues.First().Replace("Bearer", "").Trim();
                var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoUri"]);
                var database = mongoClient.GetDatabase("MongoLab-c");
                var userCollection = database.GetCollection<User>("user");
                var user = await userCollection.Find(item => item.BearerToken == token).FirstOrDefaultAsync();

                if(user == null)
                    throw new InvalidOperationException();

                return request.CreateResponse(HttpStatusCode.OK, user);
            }

            return request.CreateResponse(HttpStatusCode.Unauthorized);
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
}