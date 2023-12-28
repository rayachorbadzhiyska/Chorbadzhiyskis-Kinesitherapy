using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorbadzhiyskiKinesitherapy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "2c5e174e-3b0e-446f-86af-483d56fd7210" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3e5e174e-3b0e-446f-86af-483d56fd7210", 0, "4b8251b4-f309-43e0-a60a-f721b654e34a", null, true, false, null, null, "0876535373", "AQAAAAIAAYagAAAAEIFzAsZFMTDmS7EXy6udEZwTKREiu4W3e3A029nGIn1N6RWp2Or0raAxK0d1k2qekg==", "0876535373", true, "0734508e-6c77-4f67-ad9b-483aa4b230ad", false, "0876535373" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "3e5e174e-3b0e-446f-86af-483d56fd7210" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "3e5e174e-3b0e-446f-86af-483d56fd7210" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", 0, "4a1fb1ea-1499-4b8e-892d-6fa103e053a3", null, true, false, null, null, "0876535373", "AQAAAAIAAYagAAAAEKoxb7+ygkXbikgict4tughjJ5s/b4qmjUlKrUf5yuBbhh+pvXLDDuZ3PXzXM+EAyQ==", "0876535373", true, "85c9824b-31af-4194-a147-6b0564acd0d0", false, "0876535373" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "2c5e174e-3b0e-446f-86af-483d56fd7210" });
        }
    }
}
