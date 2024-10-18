using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterCompany.Migrations
{
    public partial class MeterDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "MeterRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "MeterRequests");
        }
    }
}
