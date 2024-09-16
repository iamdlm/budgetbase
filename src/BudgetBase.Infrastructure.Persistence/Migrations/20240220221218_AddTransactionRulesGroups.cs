using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionRulesGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransactionRulesGroupId",
                table: "TransactionRules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TransactionRulesGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRulesGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRules_TransactionRulesGroupId",
                table: "TransactionRules",
                column: "TransactionRulesGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRules_TransactionRulesGroups_TransactionRulesGro~",
                table: "TransactionRules",
                column: "TransactionRulesGroupId",
                principalTable: "TransactionRulesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRules_TransactionRulesGroups_TransactionRulesGro~",
                table: "TransactionRules");

            migrationBuilder.DropTable(
                name: "TransactionRulesGroups");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRules_TransactionRulesGroupId",
                table: "TransactionRules");

            migrationBuilder.DropColumn(
                name: "TransactionRulesGroupId",
                table: "TransactionRules");
        }
    }
}
