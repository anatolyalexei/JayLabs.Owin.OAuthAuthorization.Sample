using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace JayLabs.AuthorizationServer.Sample.Parts
{
    public class SampleOAuthBearerAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            context.Validated();

            return Task.FromResult(0);
        }
    }
}