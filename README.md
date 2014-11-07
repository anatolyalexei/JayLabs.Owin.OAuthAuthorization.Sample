JayLabs.Owin.OAuthAuthorization.Sample
==================================

Sample application, using an IIS Owin Host.

The sample is a simple OWIN Web Api application running in an IIS Host.
It serves a single file (index.html) that act's as a client.


### Azure AD
- Add a client to your provider (ex Azure AD in this sample)
- Configure the callback to - yourhost/openid 
- Add the client and tenant to web.config

### Sample project

- Controllers - Sample and Consent Controller
- Parts - Custom parts for using the JayLabs.Owin.OAuthAuthorization package
- Startup files - OWIN start up - the auth part is in an one file startup.auth.cs.


### Running the Sample

> Note the sample is pre-configured to be run **without https**.
> 
> To change this go to the *SampleAppConfiguration* class in the Parts folder.

- Run bower install
- Open the project in Visual Studio and run
- Verify configuration in the index.html to match your localhost. 

### Issuing claims

The sample issues claims based on email domain. By default all users gets the sample claim. You could change the claim and the rules as you see fit.

  	bool hasEmailClaim = userIdentityTokens.Any(
            claim =>
                claim.Type == JwtRegisteredClaimNames.Email &&
                claim.Value.EndsWith(".gov", StringComparison.InvariantCultureIgnoreCase));

        //if (hasEmailClaim) {
            claims.AddRange(userIdentityTokens);
            claims.Add(new Claim(SampleClaims.IsGovermentEmployee, "true"));
        //}


### Notes
The server uses CORS and allows all, in this sample. It's only the callback that needs to be allowed. 

