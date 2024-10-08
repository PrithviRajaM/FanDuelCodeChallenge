using Microsoft.AspNetCore.Authorization;

namespace BasicAuthentication.Shared.Authentication.Basic;

public class BasicAuthorizationAttribute : AuthorizeAttribute
{
    public BasicAuthorizationAttribute()
    {
        AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme;
    }
}
