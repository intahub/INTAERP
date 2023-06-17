using Inta.ERP.Authorization.DbContext;
using Inta.ERP.Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using System.Drawing;
using System.Net;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Inta.ERP.Authorization;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<IntaErpIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseOpenIddict();
});

// Register the Identity services.
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<IntaErpIdentityDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

//builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    //Sets the amount of time that a session can be idle before it expires
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    //This prevents client - side scripts from accessing the cookie, enhancing security by reducing the risk of cross-site scripting(XSS) attacks
    options.Cookie.HttpOnly = true;
    //  cookie can be sent with cross-site requests
    options.Cookie.SameSite = SameSiteMode.None;
    //Sets the Secure property of the session cookie. the cookie will only be sent over HTTPS connections.
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

/*************************************************************
 * will set based on the requirement*/
builder.Services.Configure<IdentityOptions>(options =>
{
    // Configure Identity to use the same JWT claims as OpenIddict instead
    // of the legacy WS-Federation claims it uses by default (ClaimTypes),
    // which saves you from doing the mapping in your authorization controller.
    options.ClaimsIdentity.UserNameClaimType = Claims.Name;
    options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
    options.ClaimsIdentity.RoleClaimType = Claims.Role;
    options.ClaimsIdentity.EmailClaimType = Claims.Email;

    // Note: to require account confirmation before login,
    // register an email sender service (IEmailSender) and
    // set options.SignIn.RequireConfirmedAccount to true.
    //
    // For more information, visit https://aka.ms/aspaccountconf.
    options.SignIn.RequireConfirmedAccount = false;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowCredentials()//cross-origin requests can include credentials, such as cookies or authorization headers
                .WithOrigins(
                    "https://localhost:4200", "https://localhost:4204")//Specifies the allowed origins for cross-origin requests
                .SetIsOriginAllowedToAllowWildcardSubdomains()// This means that any subdomain under the specified origins will be allowed
                .AllowAnyHeader()//Allows any HTTP header to be included in the cross-origin request.
                .AllowAnyMethod();//Allows any HTTP method (GET, POST, PUT, DELETE, etc.) to be used in the cross-origin request.
        });
});

// enable cookie-based authentication in your application.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

    //.AddCookie(c =>
    //{
    //    c.LoginPath = "/Login/Authenticate"; //redirects unauthenticated users
    //})
    ;


//Registers OpenIddict services in the application and configures the core options for OpenIddict
builder.Services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                       .UseDbContext<IntaErpIdentityDbContext>(); //Configures OpenIddict to use Entity Framework Core and the specified IntaErpIdentityDbContext for storing OpenIddict entities and tokens
            })
            //Adds the OpenIddict server services and configures the options for the OpenID Connect and OAuth 2.0 server
            .AddServer(options =>
            {
                // Enable the authorization, logout, token and userinfo endpoints.
                options.SetAuthorizationEndpointUris("connect/authorize") // Sets the URI for the authorization endpoint. This endpoint handles user authentication and authorization requests.                                                                              
               .SetIntrospectionEndpointUris("connect/introspect")
               .SetLogoutEndpointUris("connect/logout") //  Sets the URI for the logout endpoint. This endpoint handles user logout requests.
               .SetTokenEndpointUris("connect/token") // Sets the URI for the token endpoint. This endpoint is used by clients to exchange authorization codes or refresh tokens for access tokens.
               .SetUserinfoEndpointUris("connect/userinfo") // Sets the URI for the userinfo endpoint. This endpoint provides information about the authenticated user.
               .SetVerificationEndpointUris("connect/verify");// Sets the URI for the verification endpoint. This endpoint can be used by clients to verify the authenticity of tokens.


                options.AllowAuthorizationCodeFlow()//The authorization code flow is used by confidential clients (e.g., web applications) to obtain an authorization code from the authorization endpoint and exchange it for an access token
                .AllowRefreshTokenFlow();//The refresh token flow allows clients to obtain a new access token by presenting a valid refresh token to the token endpoint.

                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                //only for development.used to configure development certificates for encryption and signing of tokens.
                options.AddDevelopmentEncryptionCertificate()
                .AddDevelopmentSigningCertificate();

                options.UseAspNetCore() // integration for the OpenIddict server.
                       .EnableAuthorizationEndpointPassthrough()//  It allows the OpenIddict server to handle authorization requests and responses directly in the ASP.NET Core pipeline
                       .EnableLogoutEndpointPassthrough() // allows the OpenIddict server to handle logout requests and responses directly in the ASP.NET Core pipeline.
                       .EnableTokenEndpointPassthrough() // allows the OpenIddict server to handle token requests and responses directly in the ASP.NET Core pipeline.
                       .EnableUserinfoEndpointPassthrough() // allows the OpenIddict server to handle userinfo requests and responses directly in the ASP.NET Core pipeline.
                       .EnableStatusCodePagesIntegration(); // ensures that any status codes returned by the OpenIddict server are handled and displayed correctly in the ASP.NET Core application.
            }
                )
            //configure the validation services for incoming tokens
            .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore(); //  enable OpenIddict to handle the authentication and authorization-related requests and responses
                    options.EnableAuthorizationEntryValidation();
                });


var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//enable Cross-Origin Resource Sharing
app.UseCors();
//routing incoming HTTP requests to the appropriate endpoint 
app.UseRouting();
//responsible for authenticating incoming HTTP requests based on the configured authentication schemes.
app.UseAuthentication();
//enforcing access control policies and determining whether an authenticated user is authorized
app.UseAuthorization();
//route incoming HTTP requests to the appropriate controller action methods
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.Run();
