using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Models;
using PoliceOfficerManagement.Services.AuthServices;
using PoliceOfficerManagement.Services.AuthServices.Interfaces;
using PoliceOfficerManagement.Services.Dapper.IInterfaces;
using PoliceOfficerManagement.Services.jwt;
using PoliceOfficerManagement.Services.jwt.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    //options.JsonSerializerOptions.MaxDepth = 10;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
}).AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();




//#region Master Data
//builder.Services.AddScoped<IMasterDataService, MasterDataService>();
//builder.Services.AddScoped<IUploadDocumentService, UploadDocumentService>();
//#endregion

#region User Manage
builder.Services.AddScoped<IUserInfoes, UserInfoesService>();
#endregion


#region PDF DI
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
#endregion

#region Database Settings
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IDapper, PoliceOfficerManagement.Services.Dapper.Dapper>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
   .AddEntityFrameworkStores<AppDbContext>();
//.AddDefaultTokenProviders(); 

#endregion

#region Auth JWT

builder.Services.AddSingleton<IJwtFactoryService, JwtFactoryService>();
var jwtAppsettingsOptions = builder.Configuration.GetSection(nameof(JwtIssuerOptions));

SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppsettingsOptions["SecreatKey"]));

builder.Services.Configure<JwtIssuerOptions>(Options =>
{
    Options.Issuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)];
    Options.Audience = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Audience)];
    Options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
});

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)],

    ValidateAudience = true,
    ValidAudience = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Audience)],

    ValidateIssuerSigningKey = true,
    IssuerSigningKey = _signingKey,

    RequireExpirationTime = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};
#endregion


#region Auth Related Settings
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddAuthentication().AddJwtBearer(configureOptions =>
{
    configureOptions.ClaimsIssuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)];
    configureOptions.TokenValidationParameters = tokenValidationParameters;
    configureOptions.SaveToken = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);

    options.LoginPath = "/Auth/Account/Login";
    options.AccessDeniedPath = "/Auth/Account/AccessDenied";
    options.SlidingExpiration = true;
});
#endregion

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 300000000; // Set to 300 MB, for example
});

#region Areas Config
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});
#endregion

builder.Services.AddDistributedMemoryCache();//To Store session in Memory, This is default implementation of IDistributedCache    
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
     builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()
        );
});

#region App metrics
builder.Services.Configure<KestrelServerOptions>(opt => { opt.AllowSynchronousIO = true; });
//builder.Services.AddMetrics();
#endregion

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Auth/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();
app.UseCors("_myAllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();
//app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagerAPI v1"));
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    //pattern: "{area=PublicArea}/{controller=Menu}/{action=Index}/{id?}");
    pattern: "{area=Auth}/{controller=Account}/{action=Index}/{id?}");

});
//app.MapHub<OrderHub>("/orderHub");
app.Run();