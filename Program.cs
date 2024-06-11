using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Squeezer.Infrastructure;
using Squeezer.Models;
using Squeezer.Services;
using Squeezer.Services.Builders;
using Squeezer.Services.Directors;
using Squeezer.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(config => {
                            config.LoginPath = "/user/signin";
                        //    config.AccessDeniedPath = "/user/signin";
                        });

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("UserAccessible", policy => policy.RequireClaim
    ("Role", new List<string> { UserRole.Admin.ToString(), UserRole.TypicalUser.ToString() } ));
    
    option.AddPolicy("AdminOnly", policy => policy.RequireClaim
    ("Role", UserRole.Admin.ToString()));
});

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEncryptor, MD5Encryptor>();
builder.Services.AddTransient<IEncoder, Base62Encoder>();
builder.Services.AddTransient<IURLShortener, SqueezerURLShortener>();
builder.Services.AddTransient<URLManager>();
builder.Services.AddTransient<UserManager>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthenticator, ClaimBasedAuthenticator>();
builder.Services.AddTransient<IUserBuilder, UserBuilder>();
builder.Services.AddScoped<IUserDirector, UserDirector>();

builder.Services.AddDbContext<SqueezerDbContext>
    (options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqueezerDB")));


builder.Services.Configure<RouteOptions>
    (option => option.ConstraintMap.Add("shortUrlConstraint", typeof(ShortUrlRouteContstraint)));

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
