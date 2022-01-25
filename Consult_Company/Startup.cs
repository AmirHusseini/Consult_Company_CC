using Consult_Company.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Consult_Company
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
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Projects", "RequireAdministratorRole");
                    options.Conventions.AuthorizeFolder("/Uppdrags", "RequireAdministratorRole");
                    options.Conventions.AuthorizeFolder("/Admin", "RequireAdministratorRole");
                    options.Conventions.AuthorizePage("/Privacy");

                });
            services.AddAuthorization(config =>
            {
                config.AddPolicy("RequireAdministratorRole",
                    policy => policy.RequireRole("Admin"));
                config.AddPolicy("RequireMemberRole",
                    policy => policy.RequireRole("Member"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            CreateDatabase(app);
            CreateRolesAsync(serviceProvider).Wait();
            CreateSuperUser(userManager).Wait();
        }
        private async Task CreateSuperUser(UserManager<ApplicationUser> userManager)
        {
            var superUser = new ApplicationUser { firstName = "super", lastName ="admin", UserName = "superadmin@gmail.com", Email = "superadmin@gmail.com" };
            await userManager.CreateAsync(superUser, "123Pa$$word");
            var token = await userManager.GenerateEmailConfirmationTokenAsync(superUser);
            await userManager.ConfirmEmailAsync(superUser, token);
            await userManager.AddToRoleAsync(superUser, "Admin");
        }

        private void CreateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Member"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
