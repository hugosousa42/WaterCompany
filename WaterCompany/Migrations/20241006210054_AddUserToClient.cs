using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterCompany.Migrations
{
    public partial class AddUserToClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_userId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_userId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Clients",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_UserId",
                table: "Clients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_UserId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_UserId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Clients",
                newName: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_userId",
                table: "Clients",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_userId",
                table: "Clients",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
