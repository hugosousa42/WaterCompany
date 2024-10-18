using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterCompany.Migrations
{
    public partial class NewMeteResquest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "MeterRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "MeterRequests");
        }
    }
}
