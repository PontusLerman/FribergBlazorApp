using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FribergWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "realtors");

            migrationBuilder.AddColumn<string>(
                name: "Agency",
                table: "realtors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agency",
                table: "realtors");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "realtors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
