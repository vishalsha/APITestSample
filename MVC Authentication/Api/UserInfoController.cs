using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Api
{
    [Route("userinfo")]
   // [Authorize]
    public class UserInfoController : ApiController
    {
        public string Get()
        {
            return "Sunil";
            //var user = User as ClaimsPrincipal;
            //var claims = from c in user.Claims
            //             select new
            //             {
            //                 type = c.Type,
            //                 value = c.Value
            //             };

            //return Json(claims);
        }
    }
}