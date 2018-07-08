using System.Net.Http;
using System.Web.Http;

namespace Leo.Api.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("auth")]
        [Authorize]
        public IHttpActionResult Auth()
        {
            if (this.User != null)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("login")]
        public IHttpActionResult Login(string authenticationType, string redirectUrl)
        {
            return new ChallengeResult(authenticationType, redirectUrl, this.Request);
        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public IHttpActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();

            return Ok();
        }
    }
}