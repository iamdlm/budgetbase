using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixEnumTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Enum_TransactionTypeId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Enum_TransactionEntryTypeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Enum");

            migrationBuilder.CreateTable(
                name: "TransactionEntryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEntryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_TransactionTypes_TransactionTypeId",
                table: "Categories",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionEntryTypes_TransactionEntryTypeId",
                table: "Transactions",
                column: "TransactionEntryTypeId",
                principalTable: "TransactionEntryTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_TransactionTypes_TransactionTypeId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionEntryTypes_TransactionEntryTypeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionEntryTypes");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.CreateTable(
                name: "Enum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enum", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Enum_TransactionTypeId",
                table: "Categories",
                column: "TransactionTypeId",
                principalTable: "Enum",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Enum_TransactionEntryTypeId",
                table: "Transactions",
                column: "TransactionEntryTypeId",
                principalTable: "Enum",
                principalColumn: "Id");
        }
    }
}
