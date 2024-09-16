using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RecurrencyTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecurrencyTypeId",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecurrencyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrencyTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecurrencyTypeId",
                table: "Transactions",
                column: "RecurrencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_RecurrencyTypes_RecurrencyTypeId",
                table: "Transactions",
                column: "RecurrencyTypeId",
                principalTable: "RecurrencyTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_RecurrencyTypes_RecurrencyTypeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "RecurrencyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecurrencyTypeId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RecurrencyTypeId",
                table: "Transactions");
        }
    }
}
