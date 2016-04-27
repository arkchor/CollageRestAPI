using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CollageRestAPI.Hypermedia;

namespace CollageRestAPI.Controllers
{
    [RoutePrefix("api")]
    public class HomeApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetHomeApi()
        {
            return Ok(LinkManager.InitialLinks());
        }
    }
}
