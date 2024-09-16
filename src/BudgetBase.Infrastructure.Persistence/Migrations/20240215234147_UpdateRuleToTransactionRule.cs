using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRuleToTransactionRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionRuleConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRuleConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionRuleFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRuleFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TransactionRuleFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionRuleConditionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionRules_TransactionRuleConditions_TransactionRuleC~",
                        column: x => x.TransactionRuleConditionId,
                        principalTable: "TransactionRuleConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionRules_TransactionRuleFields_TransactionRuleField~",
                        column: x => x.TransactionRuleFieldId,
                        principalTable: "TransactionRuleFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRules_TransactionRuleConditionId",
                table: "TransactionRules",
                column: "TransactionRuleConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRules_TransactionRuleFieldId",
                table: "TransactionRules",
                column: "TransactionRuleFieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionRules");

            migrationBuilder.DropTable(
                name: "TransactionRuleConditions");

            migrationBuilder.DropTable(
                name: "TransactionRuleFields");
        }
    }
}
