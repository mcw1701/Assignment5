using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment5.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNetUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "NetUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Member");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "NetUser");
        }
    }
}
