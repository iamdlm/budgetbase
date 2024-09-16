using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionRuleCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransactionCategoryId",
                table: "TransactionRules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRules_TransactionCategoryId",
                table: "TransactionRules",
                column: "TransactionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRules_Categories_TransactionCategoryId",
                table: "TransactionRules",
                column: "TransactionCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRules_Categories_TransactionCategoryId",
                table: "TransactionRules");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRules_TransactionCategoryId",
                table: "TransactionRules");

            migrationBuilder.DropColumn(
                name: "TransactionCategoryId",
                table: "TransactionRules");
        }
    }
}
