using Microsoft.EntityFrameworkCore;
using Squeezer.Infrastructure;
using Squeezer.Services;
using Squeezer.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEncryptor, MD5Encryptor>();
builder.Services.AddTransient<IEncoder, Base62Encoder>();
builder.Services.AddTransient<IURLShortener, SqueezerURLShortener>();

var urlManagerServiceDescriptor = new ServiceDescriptor
    (typeof(URLManager), typeof(URLManager), ServiceLifetime.Transient);
builder.Services.Add(urlManagerServiceDescriptor);

var userManagerServiceDescriptor = new ServiceDescriptor
    (typeof(UserManager), typeof(UserManager), ServiceLifetime.Transient);
builder.Services.Add(userManagerServiceDescriptor);

builder.Services.AddDbContext<SqueezerDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqueezerDB")));

builder.Services.Configure<RouteOptions>(option => option.ConstraintMap.Add("shortUrlConstraint", typeof(ShortUrlRouteContstraint)));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
