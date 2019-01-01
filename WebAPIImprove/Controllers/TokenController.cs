using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPIImprove.Identity;
using WebAPIImprove.Models;

namespace WebAPIImprove.Controllers
{
    public class TokenController : ApiController
    {
        // GET: Token
        [HttpPost]
        public IHttpActionResult GetToken(APIUser user)
        {
            //Check the credentials as per your requirment
            if (ModelState.IsValid)
            {
                if (user.UserName=="ABC" && user.Password=="Password1.")
                {
                    string token = JwtToken.createToken(user.UserName); return Ok<string>(token);
                }
                else
                {
                    return BadRequest("Invalid Credintials");
                }                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}