using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRulesGroupOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransactionRulesGroupOperatorId",
                table: "TransactionRulesGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TransactionRulesGroupOperator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRulesGroupOperator", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRulesGroups_TransactionRulesGroupOperatorId",
                table: "TransactionRulesGroups",
                column: "TransactionRulesGroupOperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRulesGroups_TransactionRulesGroupOperator_Transa~",
                table: "TransactionRulesGroups",
                column: "TransactionRulesGroupOperatorId",
                principalTable: "TransactionRulesGroupOperator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRulesGroups_TransactionRulesGroupOperator_Transa~",
                table: "TransactionRulesGroups");

            migrationBuilder.DropTable(
                name: "TransactionRulesGroupOperator");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRulesGroups_TransactionRulesGroupOperatorId",
                table: "TransactionRulesGroups");

            migrationBuilder.DropColumn(
                name: "TransactionRulesGroupOperatorId",
                table: "TransactionRulesGroups");
        }
    }
}
