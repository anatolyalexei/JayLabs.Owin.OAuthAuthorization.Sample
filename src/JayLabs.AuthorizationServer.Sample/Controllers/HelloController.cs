using System.Web.Http;
using JayLabs.AuthorizationServer.Sample.Parts;
using JayLabs.Owin.OAuthAuthorization;

namespace JayLabs.AuthorizationServer.Sample.Controllers
{
    [RoutePrefix("Hello")]
    [ClaimAuthorize(SampleClaims.IsGovermentEmployee)]
    public class HelloController : ApiController
    {
        [Route("")]
         public string Get()
        {
            return "Hello there";
        }
    }
}