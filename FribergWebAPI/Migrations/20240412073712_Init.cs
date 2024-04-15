using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FribergWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LivingArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BiArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlotArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ObjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    MonthlyFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperatingCostPerYear = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConstructionYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Residences");
        }
    }
}
