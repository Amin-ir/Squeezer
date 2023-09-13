using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Squeezer.Components.ProfileBadge;
using Squeezer.Infrastructure;
using Squeezer.Services;
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
    option.AddPolicy("UserAccessible", policy => policy.RequireClaim("Role", "SignedUser"));
});

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEncryptor, MD5Encryptor>();
builder.Services.AddTransient<IEncoder, Base62Encoder>();
builder.Services.AddTransient<IURLShortener, SqueezerURLShortener>();
builder.Services.AddTransient<URLManager, URLManager>();
builder.Services.AddTransient<UserManager,UserManager>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthenticator, ClaimBasedAuthenticator>();
builder.Services.AddScoped<ProfileBadgeViewComponent>();

builder.Services.AddDbContext<SqueezerDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqueezerDB")));

builder.Services.Configure<RouteOptions>
    (option => option.ConstraintMap.Add("shortUrlConstraint", typeof(ShortUrlRouteContstraint)));

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
