using Microsoft.AspNetCore.Authentication;
using BasicAuthentication.Shared.Authentication.Basic;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JwtBearer.Shared.Authentication;
using Microsoft.Extensions.Caching.Memory;
using DepthChart_Business.Interfaces;
using DepthChart_Business.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //Detailing the Swagger interface display doc
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Depth Chart API", Description = "A simple API to manage NFL Depth Chart .", Version = "v1" });

    //Defining to collect username and password to relay while creating bearer token
    options.AddSecurityDefinition(BasicAuthenticationDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = BasicAuthenticationDefaults.AuthenticationScheme,
        In = ParameterLocation.Header,
        Description = "Basic Authorization header.\r\n\r\nEnter the client ID as the Username, and the plain client secret as the password",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {

                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = BasicAuthenticationDefaults.AuthenticationScheme
                }
            },
            new string[] { "Basic "}
        }
    });
    //Collect the generated bearer token to relay on all endpoint calls for authentication
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer authentication scheme.\r\n\r\nEnter your token in the text input below.",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {

                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] { "Bearer "}
        }
    });
});

builder.Services.AddOptions<JwtBearerSettings>()
    .Bind(builder.Configuration.GetSection("JwtBearer"))
    .ValidateDataAnnotations();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BasicAuthenticationDefaults.AuthenticationScheme, null)
.AddScheme<JwtBearerOptions, JwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var jwtBearerSettings = builder.Configuration.GetSection("JwtBearer").Get<JwtBearerSettings>();

    if (jwtBearerSettings == null)
    {
        // Cannot find JWT Bearer Settings settings, so throw exception
        throw new NullReferenceException("The 'JwtBearer' section cannot be found in the configuration");
    }
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = jwtBearerSettings.Issuer,
        ValidAudience = jwtBearerSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearerSettings.SigningKey)),
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DepthChartAPI v1");
        options.RoutePrefix = string.Empty;
    });
}

builder.Services.AddScoped<IPlayerBusiness, PlayerBusiness>();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
