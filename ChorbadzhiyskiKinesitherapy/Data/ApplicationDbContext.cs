using ChorbadzhiyskiKinesitherapy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChorbadzhiyskiKinesitherapy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding an 'Administrator' role to AspNetRoles table.
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );

            // The keys have to be predefined to avoid seeding new users and roles everytime this method is executed.
            var adminUser = new ApplicationUser
            {
                Id = "3e5e174e-3b0e-446f-86af-483d56fd7210",
                UserName = "0876535373",
                NormalizedUserName = "0876535373",
                PhoneNumber = "0876535373",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign the Admin role to the admin user
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210" }
            );
        }
    }
}