using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProject009.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB009 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MiniProject009_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniProject009_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiniProject009_Expenditures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenditureDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpenditureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenditureAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniProject009_Expenditures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MiniProject009_Expenditures_MiniProject009_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MiniProject009_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MiniProject009_Expenditures_CategoryId",
                table: "MiniProject009_Expenditures",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MiniProject009_Expenditures");

            migrationBuilder.DropTable(
                name: "MiniProject009_Categories");
        }
    }
}
