#nullable enable
using System.Security.Principal;

namespace BasicAuthentication.Shared.Authentication.Basic;

public class BasicAuthenticationClient : IIdentity
{
    public string? AuthenticationType { get; set; }

    public bool IsAuthenticated { get; set; }

    public string? Name { get; set; }
}
