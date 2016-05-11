using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CollageRestAPI.Hypermedia;

namespace CollageRestAPI.Controllers
{
    //[RoutePrefix("api/start")]
    public class HomeApiController : ApiController
    {
        [HttpGet]
        [Route("api/start")]
        public IHttpActionResult GetHomeApi()
        {        
            return Ok(LinkManager.InitialLinks(Url));
        }
    }
}
