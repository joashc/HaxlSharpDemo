using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HaxlSharp;

namespace HaxlSharp.WebApi.Controllers
{
    public class PostsController : ApiController
    {
        // GET: api/Posts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Posts/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Posts
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Posts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Posts/5
        public void Delete(int id)
        {
        }
    }
}
