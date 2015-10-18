using System;
using System.Web.Http;

namespace Web.Controllers
{
    [RoutePrefix("api/stuffs")]
    public class StuffsController : ApiController
    {
        [HttpGet]
        [Route("throwexception")]
        public void ThrowException()
        {
            throw new InvalidOperationException("no can do");
        }
    }
}