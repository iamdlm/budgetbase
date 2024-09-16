using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionEntryTypeId",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionTypeId",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Enum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enum", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionEntryTypeId",
                table: "Transactions",
                column: "TransactionEntryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TransactionTypeId",
                table: "Categories",
                column: "TransactionTypeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Enum_TransactionTypeId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Enum_TransactionEntryTypeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Enum");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionEntryTypeId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TransactionTypeId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TransactionEntryTypeId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "EntryType",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
