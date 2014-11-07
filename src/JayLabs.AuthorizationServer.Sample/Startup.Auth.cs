using System;
using System.Threading.Tasks;
using JayLabs.AuthorizationServer.Sample.Parts;
using JayLabs.Owin.OAuthAuthorization;
using JayLabs.Owin.OAuthAuthorization.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace JayLabs.AuthorizationServer.Sample
{
    public static class StartupAuth
    {
        public static void Configuration(IAppBuilder app)
        {
            var appConfiguration = SampleAppConfiguration.CreateFromConfigFile(prefix: "JayLabs");

            var jwtOptions = SampleJwtOptions.JwtOptions;

            var jwtBearerOptions = new JwtBearerTokenAuthenticationOptions(jwtOptions);

            jwtBearerOptions.JwtBearerOptions.Provider = new SampleOAuthBearerAuthenticationProvider();

            app.UseJwtBearerAuthenticationWithTokenProvider(jwtBearerOptions);

            var customProviderOptions = SampleOptionsHelper.CreateOptions(jwtOptions);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AccessTokenFormat = jwtBearerOptions.JwtFormat,
                ApplicationCanDisplayErrors = true,
                Provider = new CustomOAuthProvider(customProviderOptions), //TODO JwtOptions is placeholder
                AuthorizeEndpointPath = new PathString("/authorize"),
                AllowInsecureHttp = appConfiguration.AllowInsecureHttp,
                AccessTokenProvider = new JwtBearerTokenProvider(jwtOptions)
            });

            var createConsentOptions = new CreateConsentOptions
            {
                CreateConsentAsync = (response, redirectUri) =>
                {
                    var consentUrl = new Uri(string.Format("/consent?redirectUri={0}&consentParamName={1}",
                        Uri.EscapeDataString(redirectUri.ToString()),
                        Uri.EscapeDataString(customProviderOptions.HandleConsentOptions.ConsentParameterName)),
                        UriKind.Relative);

                    response.Redirect(consentUrl.ToString());

                    return Task.FromResult(0);
                }
            };

            var consentBuilder = new ConsentBuilder(createConsentOptions, customProviderOptions.HandleConsentOptions,
                jwtOptions);

            var notifications = new OpenIdConnectAuthenticationNotifications
            {
                AuthorizationCodeReceived = consentBuilder.HandleOpenIdAuthorizationCodeAsync
            };
            var openIdConnectOptions = new OpenIdConnectAuthenticationOptions
            {
                ClientId = appConfiguration.OpenIdClientId,
                Authority = appConfiguration.OpenIdAuthority,
                CallbackPath = new PathString("/openid"),
                Notifications = notifications,
                AuthenticationMode = AuthenticationMode.Active
            };

            app.UseOpenIdConnectAuthentication(openIdConnectOptions);

            app.SetDefaultSignInAsAuthenticationType(OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }
    }
}