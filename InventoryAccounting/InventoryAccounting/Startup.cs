using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using InventoryAccounting.Filters;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;
using InventoryAccounting.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace InventoryAccounting
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<InventoryAccountingContext>(options =>
                options.UseSqlServer(connection));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<InventoryAccountingContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
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

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout"; // If the LogoutPath is not set here,
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]
                ));
            services.AddScoped<ValidateEntityExistsAttribute<Rooms>>();
            services.AddScoped<ValidateEntityExistsAttribute<Tmc>>();
            services.AddScoped<ValidateEntityExistsAttribute<TmcTypes>>();
            services.AddScoped<ValidateEntityExistsAttribute<Persons>>();
            services.AddScoped<ValidateEntityExistsAttribute<Acts>>();
            services.AddScoped<ValidateEntityExistsAttribute<Contracts>>();
            services.AddScoped<ValidateEntityExistsAttribute<Companies>>();

            /**** Localization configuration ****/
            services.AddSingleton<IdentityLocalizationService>();
            services.AddSingleton<SharedLocalizationService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ru-RU"),
                        new CultureInfo("de-CH")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "ru-RU", uiCulture: "ru-RU");
                    //options.SetDefaultCulture("de-CH");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new List<IRequestCultureProvider>
                    {
                        new QueryStringRequestCultureProvider(),
                        new CookieRequestCultureProvider()
                    };
                });

            services.AddMvc(options => { 
                options.Filters.Add(typeof(ValidateModelAttribute));
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(val => $"Значение '{val}' недопустимо.");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(val => $"Значение для свойства '{val}' не было предоставлено.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Значение обязательное.");
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "Требуется непустое тело запроса.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(val => $"Значение '{val}' недопустимо.");
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((v,p) => $"Значение '{v}' недопустимо для {p}.");
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(val => $"Значение '{val}' недопустимо.");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(val => $"Предоставленное значение недопустимо для {val}.");
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "Предоставленное значение недействительно.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(val => $"Поле {val} должно быть числом.");
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "Поле должно быть числом.");
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(IdentityResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("IdentityResource", assemblyName.Name);
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //CreateUserRoles(services).Wait();
        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            //User user = await UserManager.FindByEmailAsync("antondedenko@gmail.com");
            //var User = new User();
            //await UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
