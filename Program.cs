using Squeezer.Services.Encoder;
using Squeezer.Services.Encryptor;
using Squeezer.Services.URLShortener;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEncryptor, MD5Encryptor>();
builder.Services.AddTransient<IEncoder, Base62Encoder>();
builder.Services.AddTransient<IURLShortener, SqueezerURLShortener>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
