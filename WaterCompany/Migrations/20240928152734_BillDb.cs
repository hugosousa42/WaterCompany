using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterCompany.Migrations
{
    public partial class BillDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Clients",
                newName: "Birthdate");

            migrationBuilder.CreateTable(
                name: "BillDetailsTemp",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Clientid = table.Column<int>(type: "int", nullable: true),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetailsTemp", x => x.id);
                    table.ForeignKey(
                        name: "FK_BillDetailsTemp_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillDetailsTemp_Clients_Clientid",
                        column: x => x.Clientid,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.id);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clientid = table.Column<int>(type: "int", nullable: true),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Billid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_Billid",
                        column: x => x.Billid,
                        principalTable: "Bills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillDetails_Clients_Clientid",
                        column: x => x.Clientid,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_Billid",
                table: "BillDetails",
                column: "Billid");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_Clientid",
                table: "BillDetails",
                column: "Clientid");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetailsTemp_Clientid",
                table: "BillDetailsTemp",
                column: "Clientid");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetailsTemp_UserId",
                table: "BillDetailsTemp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "BillDetailsTemp");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Clients",
                newName: "RegistrationDate");
        }
    }
}
