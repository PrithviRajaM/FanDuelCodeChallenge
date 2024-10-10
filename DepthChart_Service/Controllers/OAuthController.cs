using BasicAuthentication.Shared.Authentication.Basic;
using JwtBearer.Shared.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DepthChart_Service.Controllers;

/// <summary>
/// This controller is to manage the Authentication and generation of the bearer token
/// </summary>
[Route("[controller]")]
public class OAuthController : Controller
{
    private readonly JwtBearerSettings _jwtBearerSettings;

    public OAuthController(IOptions<JwtBearerSettings> jwtBearerSettingsOptions)
    {
        _jwtBearerSettings = jwtBearerSettingsOptions.Value;
    }

    /// <summary>
    /// This endpoint generate the bearer token based on the credentials provided
    /// </summary>
    /// <param name="grantType">TO make sure client credentials are meant</param>
    /// <returns></returns>
    [HttpPost("token"), BasicAuthorization, Consumes("application/x-www-form-urlencoded")]
    public IActionResult Token([FromForm(Name = "grant_type")] string grantType)
    {
        if (grantType != "client_credentials")
        {
            //Grant type must be set as 'client_credentials' otherwise it throws a bad request
            return BadRequest(new { 
                error = "invalid_grant", 
                error_description = "The grant type form value must be set as 'client_credentials'" 
            });
        }

        //Generate JWT Token
        var tokenHandler = new JwtSecurityTokenHandler();

        var now = DateTime.UtcNow;
        var expiry = now.Add(TimeSpan.FromHours(1));

        //Creating the clent paramters
        var jwtBearerAuthenticatedClient = new JwtBearerClient
        {
            IsAuthenticated = true,
            AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
            Name = "FanDuel"
        };

        //Write the token and return it.
        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(jwtBearerAuthenticatedClient, new List<Claim>
                {
                    { new Claim(JwtRegisteredClaimNames.Name, "FanDuel") }
                }),
            Expires = expiry,
            Issuer = _jwtBearerSettings.Issuer,
            Audience = _jwtBearerSettings.Audience,
            SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerSettings.SigningKey)), 
                    SecurityAlgorithms.HmacSha512Signature
                ),
            IssuedAt = now,
            NotBefore = now,
        }));

        return Ok(new { 
            access_token = token, 
            token_type = JwtBearerDefaults.AuthenticationScheme, 
            expires_in = expiry.Subtract(DateTime.UtcNow).TotalSeconds.ToString("0") 
        });
    }
}
