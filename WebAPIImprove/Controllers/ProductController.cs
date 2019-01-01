using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPIImprove.Interfaces;

namespace WebAPIImprove.Controllers
{
    public class ProductController : ApiController
    {
        private IProduct _product;


        public ProductController(IProduct product)
        {
            this._product = product;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            var strPrdDescription = _product.GetProductDesciption();
            return strPrdDescription;
        }

        // POST api/values
        [System.Web.Http.Authorize]
        public async Task<IHttpActionResult> Post([FromBody]string value)
        {
            return Ok("You are Authorized");
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}