using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using JayLabs.Owin.OAuthAuthorization;

namespace JayLabs.AuthorizationServer.Sample.Controllers
{
    [RoutePrefix("consent")]
    public class ConsentController : ApiController
    {
        [HttpGet]
        [Route]
        public HttpResponseMessage Get(string redirectUri = "", string consentParamName = "")
        {
            OAuthRequest oAuthRequest = OAuthRequest.Parse(new Uri(redirectUri));

            const string htmlFormat = @"<!DOCTYPE html>
<html>
<head>
<title>Consent</title>

<link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css"">

<link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap-theme.min.css"">


</head>

<body>

 <div class=""navbar navbar-inverse navbar-fixed-top"" role=""navigation"">
      <div class=""container"">
        <div class=""navbar-header"">
          <button type=""button"" class=""navbar-toggle collapsed"" data-toggle=""collapse"" data-target="".navbar-collapse"">
            <span class=""sr-only"">Toggle navigation</span>            
          </button>
          <a class=""navbar-brand"" href=""#"">Home</a>
        </div>
        <div class=""collapse navbar-collapse"">
          <ul class=""nav navbar-nav"">
            <li class=""active""><a href=""#"">Home</a></li>
          </ul>
        </div>
      </div>
    </div>

 <div class=""container"">

      <div style=""padding-top: 40px; box-sizing: border-box;"">
        
<h1>The client {0} application wants you to grant access to the server (scope: {1}). Do you accept?</h1>

<form method=""POST"" action=""{2}"">

<input type=""submit"" value=""Accept"" style=""color: green;"" />

<input type=""hidden"" name=""jwt_token"" value=""{3}"" />
<input type=""hidden"" name=""{4}"" value=""accepted"" />

</form>

<form method=""POST"" action=""{2}"">

<input type=""submit"" value=""Reject"" style=""color: red;"" />

<input type=""hidden"" name=""jwt_token"" value=""{3}"" />
<input type=""hidden"" name=""{4}"" value=""rejected"" />

</form>

      </div>

    </div>


<script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js""></script>
</body>

</html>";

            string htmlFormatted = string.Format(htmlFormat, oAuthRequest.ClientId, oAuthRequest.Scope,
                oAuthRequest.AuthorizeUri, oAuthRequest.Jwt, consentParamName);

            var stringContent = new StringContent(htmlFormatted, Encoding.UTF8, "text/html");

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = stringContent
            };

            return httpResponseMessage;
        }
    }
}