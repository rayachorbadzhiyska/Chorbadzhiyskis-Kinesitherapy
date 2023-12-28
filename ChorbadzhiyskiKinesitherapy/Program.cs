using ChorbadzhiyskiKinesitherapy.Data;
using ChorbadzhiyskiKinesitherapy.Models;
using ChorbadzhiyskiKinesitherapy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChorbadzhiyskiKinesitherapy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            var patientsDatabaseConnectionString = builder.Configuration.GetSection("PatientsDatabase").GetValue<string>("ConnectionString") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
            var identityDatabaseConnectionString = builder.Configuration.GetValue<string>("IdentityDatabaseConnectionString") ?? throw new InvalidOperationException("Connection string 'IdentityDatabaseConnectionString' not found.");

            // Add services to the container.
            builder.Services.Configure<PatientsDatabaseSettings>(
                builder.Configuration.GetSection("PatientsDatabase"));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(identityDatabaseConnectionString));

            builder
                .Services
                .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddSingleton<PatientsService>();

            builder.Services.AddControllers().AddJsonOptions(
                options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSwaggerGen();

            builder.Services.AddAntiforgery(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseWebSockets();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.None,
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Patient}/{action=Index}/{id?}");

            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}