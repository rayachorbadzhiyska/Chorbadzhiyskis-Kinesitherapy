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
			var connectionString = builder.Configuration.GetSection("PatientsDatabase").GetValue<string>("ConnectionString") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

			// Add services to the container.
			builder.Services.Configure<PatientsDatabaseSettings>(
				builder.Configuration.GetSection("PatientsDatabase"));

			builder.Services.AddDbContext<ApplicationDbContext>();
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddSingleton<PatientsService>();

			builder.Services.AddControllers().AddJsonOptions(
				options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

			builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}