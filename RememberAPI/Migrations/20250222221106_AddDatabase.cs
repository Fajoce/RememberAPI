using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RememberAPI.Migrations
{
    public partial class AddDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payrolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payrolls_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[] { 1, "Gerencia" });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[] { 2, "Ventas" });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[] { 3, "Produccion" });

            migrationBuilder.CreateIndex(
                name: "IX_payrolls_DepartmentId",
                table: "payrolls",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payrolls");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
