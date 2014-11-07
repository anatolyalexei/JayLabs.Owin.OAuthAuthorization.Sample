using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JayLabs.Owin.OAuthAuthorization;
using JayLabs.Owin.OAuthAuthorization.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.JwtRegisteredClaimNames;

namespace JayLabs.AuthorizationServer.Sample
{
    public static class SampleOptionsHelper
    {
        public static CustomProviderOptions CreateOptions(JwtOptions jwtOptions)
        {
            var handleConsentOptions = new HandleConsentOptions(consentParameterName: "consentAnswer");

            return new CustomProviderOptions(jwtOptions, handleConsentOptions)
            {
                TransformPrincipal =
                    principal =>
                    {
                        var claims = new List<Claim>();

                        List<Claim> userIdentityTokens =
                            principal.Claims
                                .Where(claim =>
                                    claim.Type == ClaimTypes.Name || claim.Type == ClaimTypes.NameIdentifier ||
                                    claim.Type == JwtRegisteredClaimNames.UniqueName ||
                                    claim.Type == JwtRegisteredClaimNames.Email)
                                .ToList();

                        bool hasEmailClaim = userIdentityTokens.Any(
                            claim =>
                                claim.Type == JwtRegisteredClaimNames.Email &&
                                claim.Value.EndsWith(".gov", StringComparison.InvariantCultureIgnoreCase));

                        //if (hasEmailClaim) {
                            claims.AddRange(userIdentityTokens);
                            claims.Add(new Claim(SampleClaims.IsGovermentEmployee, "true"));
                        //}

                        return Task.FromResult(new ClaimsIdentity(claims, "JayLabs"));
                    }
            };
        }
    }
}