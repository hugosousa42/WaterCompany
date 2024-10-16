using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterCompany.Migrations
{
    public partial class NewBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Clients_Clientid",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetailsTemp_Clients_ClientId",
                table: "BillDetailsTemp");

            migrationBuilder.DropIndex(
                name: "IX_BillDetailsTemp_ClientId",
                table: "BillDetailsTemp");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_Clientid",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "BillDetailsTemp");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BillDetailsTemp");

            migrationBuilder.DropColumn(
                name: "Clientid",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BillDetails");

            migrationBuilder.AddColumn<int>(
                name: "Method",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BillDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_UserId",
                table: "BillDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_AspNetUsers_UserId",
                table: "BillDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_AspNetUsers_UserId",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_UserId",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BillDetails");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "BillDetailsTemp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BillDetailsTemp",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Clientid",
                table: "BillDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BillDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetailsTemp_ClientId",
                table: "BillDetailsTemp",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_Clientid",
                table: "BillDetails",
                column: "Clientid");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Clients_Clientid",
                table: "BillDetails",
                column: "Clientid",
                principalTable: "Clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetailsTemp_Clients_ClientId",
                table: "BillDetailsTemp",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
