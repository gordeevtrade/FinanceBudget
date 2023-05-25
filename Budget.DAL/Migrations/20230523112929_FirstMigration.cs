using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_CategoryTypes_CategoryTypeId",
                        column: x => x.CategoryTypeId,
                        principalTable: "CategoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinacneTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinacneTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinacneTransactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Income" },
                    { 2, "Expense" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryTypeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Salary" },
                    { 2, 1, "Stock" },
                    { 3, 1, "Houses" },
                    { 4, 2, "Groceries" },
                    { 5, 2, "Собаки" }
                });

            migrationBuilder.InsertData(
                table: "FinacneTransactions",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Note" },
                values: new object[,]
                {
                    { 1, 1000m, 1, new DateTime(2023, 5, 23, 14, 29, 29, 557, DateTimeKind.Local).AddTicks(6986), "Salary for May" },
                    { 2, 950m, 2, new DateTime(2023, 5, 23, 14, 29, 29, 557, DateTimeKind.Local).AddTicks(7031), "Microsoft" },
                    { 3, 1850m, 3, new DateTime(2023, 5, 23, 14, 29, 29, 557, DateTimeKind.Local).AddTicks(7038), "Avalon" },
                    { 4, -200m, 4, new DateTime(2023, 5, 23, 14, 29, 29, 557, DateTimeKind.Local).AddTicks(7044), "Grocery shopping" },
                    { 5, -8700m, 5, new DateTime(2023, 5, 23, 14, 29, 29, 557, DateTimeKind.Local).AddTicks(7049), "Собаки " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryTypeId",
                table: "Categories",
                column: "CategoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinacneTransactions_CategoryId",
                table: "FinacneTransactions",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinacneTransactions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryTypes");
        }
    }
}
