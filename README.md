GNaP.Owin.Authentication.Jwt
============================

## Usage

Install from NuGet in your OWIN project: ```Install-Package GNaP.Owin.Authentication.Jwt```

You can now use ```app.UseJwtTokenIssuer()``` in your OWIN configuration.

## Example

```csharp
app.UseJwtTokenIssuer(new JwtTokenIssuerOptions
{
    Issuer = "urn:issuer",
    Audience = "urn:audience",
    TokenSigningKey = "U0lHTklOR19LRVlfR09FU19IRVJF",
    Authenticate = (username, password) =>
    {
        // Dummy example authentication check
        if (username.Equals("gnap"))
        {
            return new[]
            {
                new Claim(ClaimTypes.AuthenticationInstant, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")),
                new Claim(ClaimTypes.AuthenticationMethod, AuthenticationTypes.Password), 
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Administrator")
            };
        }

        // Invalid user
        return null;
    }
});
```

## Request

```
POST /tokens HTTP/1.1
Content-Type: application/json
Accept: application/json

{ "username": "gnap", "password": "super secure pass!" }
```

## Response

```
HTTP/1.1 200 OK
Content-Type: application/json

{"token":"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdXRoX3RpbWUiOiIyMDE0LTEyLTA1VDIzOjAyOjE3Ljc3OFoiLCJhdXRobWV0aG9kIjoiUGFzc3dvcmQiLCJ1bmlxdWVfbmFtZSI6ImduYXAiLCJyb2xlIjoiQWRtaW5pc3RyYXRvciIsImlzcyI6InVybjppc3N1ZXIiLCJhdWQiOiJ1cm46YXVkaWVuY2UiLCJleHAiOjE0MTc4MjQxMzd9.YbEKE2Jktssh47uwbWwEM5MQmiunrrA4s7Umrm_9Fv8"}
```

## Debugging

You can paste the returned token in [jwt.io](http://jwt.io/) which will show you the content of the JWT.

![jwt.io debug](https://raw.githubusercontent.com/infrabel/GNaP.Owin.Authentication.Jwt/master/jwt.png)

## Options

### Issuer, Audience

Self-explanatory, the issuer and audience for the JWT.

### TokenSigningKey

A Base64 encoded secret. This secret should only be known on the server.

### IssuerPath

The REST endpoint from where to issue tokens from.

**Default**: /tokens

### TokenLifetime

The lifetime of the JWT.

**Default**: 1 hour

### Authenticate

A callback method which receives the username and password to be used in the authentication process.

If the user is valid, a list of claims should be returned.

If the user is invalid, return ```null```.

## NuGet

[GNaP.Owin.Authentication.Jwt](http://www.nuget.org/packages/GNaP.Owin.Authentication.Jwt/)

## Copyright

Copyright © 2014 Infrabel and contributors.

## License

GNaP.Owin.Authentication.Jwt is licensed under [BSD (3-Clause)](http://choosealicense.com/licenses/bsd-3-clause/ "Read more about the BSD (3-Clause) License"). Refer to [LICENSE](https://github.com/infrabel/GNaP.Owin.Authentication.Jwt/blob/master/LICENSE) for more information.
