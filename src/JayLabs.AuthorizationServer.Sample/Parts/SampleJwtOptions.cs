using JayLabs.Owin.OAuthAuthorization.Tokens;

namespace JayLabs.AuthorizationServer.Sample.Parts
{
    public static class SampleJwtOptions
    {
        public static string JwtSigningKeyAsUtf8
        {
            get { return "a random crypto key"; }
        }

        public static string Issuer
        {
            get { return "JayLabs"; }
        }

        public static string Audience
        {
            get { return "https://localhost:44309"; }
        }

        public static string JwtTokenHeader
        {
            get { return "jwt_token"; }
        }

        public static JwtOptions JwtOptions
        {
            get
            {
                return new JwtOptions { Audience = Audience, Issuer = Issuer, JwtSigningKeyAsUtf8 = JwtSigningKeyAsUtf8, JwtTokenParameterName = JwtTokenHeader, SupportedScope = "JayLabs" };
            }
        }
    }
}