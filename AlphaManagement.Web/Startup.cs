using System;
using System.Security.Claims;
using System.Text;
using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Models;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmailService;
using AlphaManagement.Domain.EmailService.interfaces;
using AlphaManagement.Domain.EmployeeService;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.EmployeeService.Interfaces;
using AlphaManagement.Domain.MasterDataServices;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Domain.MasterDataServices.Repository;
using AlphaManagement.Domain.OgranogramService;
using AlphaManagement.Domain.OgranogramService.Interfaces;
using AlphaManagement.Domain.SMSService;
using AlphaManagement.Domain.SMSService.interfaces;
using AlphaManagement.Web.Helpers;
using AlphaManagement.Web.JWT_Service;
using AlphaManagement.Web.JWT_Service.Interfaces;
using AlphaManagement.Web.PushNotification.Models;
using AlphaManagement.Web.PushNotification.Services;
using CorePush.Apple;
using CorePush.Google;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using AlphaManagement.Domain.RepositoryService;

namespace AlphaManagement.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    var resolver = options.SerializerSettings.ContractResolver;
                    if (resolver != null)
                        (resolver as DefaultContractResolver).NamingStrategy = null;
                });

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromHours(24);
                options.Cookie.IsEssential = true;
            });

            services.AddHttpContextAccessor();
            
            #region ERP Database Settings
            services.AddDbContext<AlphaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AlphaConnection")));

            services.AddMemoryCache();
            //.AddDefaultTokenProviders();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<AlphaDbContext>().AddDefaultTokenProviders();
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ERPDbContext>();
            #endregion

            #region Auth JWT

            services.AddSingleton<IJwtFactoryService, JwtFactoryService>();
            var jwtAppsettingsOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppsettingsOptions["SecreatKey"]));

            services.Configure<JwtIssuerOptions>(Options =>
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
            services.Configure<IdentityOptions>(options =>
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
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(40);
                //options.ExpireTimeSpan = TimeSpan.FromSeconds(20);

                options.LoginPath = "/Auth/Account/Login";
                //options.LoginPath = "/Auth/Account/UserLogin";
                options.AccessDeniedPath = "/Home/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthentication().AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });
            
            //services.AddAuthorization(Options =>
            //{
            //    Options.AddPolicy("AlphaMobile", policy => policy.RequireClaim("Roles", "IGP"));
            //    Options.AddPolicy("AlphaMobile", policy => policy.RequireClaim("Roles", "Super Admin"));
            //});

            #endregion

            #region Areas Config
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
            #endregion

            #region PDF
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion

            #region Master Data
            services.AddScoped<IUserInfoes, UserInfoes>();
            services.AddScoped<INavbarService, NavbarService>();
            services.AddScoped<IAccessLogHistoryService, AccessLogHistoryService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<ISMSService, SMSService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            #endregion

            #region Employee
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IPortfolioDashBoard, PortfolioDashBoardService>();
            #endregion

            #region MasterDataService
            services.AddScoped<IDegreeService, DegreeService>();
            services.AddScoped<IAddressServices, AddressServices>();
            services.AddScoped<IPhotographService, PhotographService>();
            services.AddScoped<ISpecialBranchUnitServices, SpecialBranchUnitServices>();
            #endregion

            #region Organogram
            services.AddScoped<IOrganizationPostService, OrganizationPostService>();
            #endregion

            #region Pending Report
            services.AddScoped<IReportService, ReportService>();
            #endregion

            #region Internal
            services.AddScoped<IInternalPostingServices, InternalPostingServices>();
            #endregion
            services.Configure<FormOptions>(x => x.ValueCountLimit = 10000);

            #region PUSH Notification

            services.AddTransient<INotificationService, NotificationService>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("FcmNotification");
            services.Configure<FcmNotificationSetting>(appSettingsSection);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEncryptDecryptQueryStringsMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            

            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
