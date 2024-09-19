using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    IBAN = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    TransactionTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentTransactionCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentTransactionCategoryId",
                        column: x => x.ParentTransactionCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Imports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InsertDuplicates = table.Column<bool>(type: "boolean", nullable: false),
                    IgnoreRules = table.Column<bool>(type: "boolean", nullable: false),
                    Filename = table.Column<string>(type: "text", nullable: false),
                    TransactionsCount = table.Column<int>(type: "integer", nullable: false),
                    TransactionsInserted = table.Column<int>(type: "integer", nullable: false),
                    BankId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imports_Accounts_SourceAccountId",
                        column: x => x.SourceAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Imports_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransactionRulesGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TransactionCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionRulesGroupOperatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRulesGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionRulesGroups_Categories_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionRulesGroups_TransactionRulesGroupOperator_Transa~",
                        column: x => x.TransactionRulesGroupOperatorId,
                        principalTable: "TransactionRulesGroupOperator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    SourceAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    TransactionEntryTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    RecurrencyTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    TransactionCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_SourceAccountId",
                        column: x => x.SourceAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_TargetAccountId",
                        column: x => x.TargetAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_RecurrencyTypes_RecurrencyTypeId",
                        column: x => x.RecurrencyTypeId,
                        principalTable: "RecurrencyTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionEntryTypes_TransactionEntryTypeId",
                        column: x => x.TransactionEntryTypeId,
                        principalTable: "TransactionEntryTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransactionRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    TransactionRuleFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionRuleConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionRulesGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_TransactionRules_TransactionRulesGroups_TransactionRulesGro~",
                        column: x => x.TransactionRulesGroupId,
                        principalTable: "TransactionRulesGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionImports",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImportId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionImports", x => new { x.TransactionId, x.ImportId });
                    table.ForeignKey(
                        name: "FK_TransactionImports_Imports_ImportId",
                        column: x => x.ImportId,
                        principalTable: "Imports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionImports_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CountryId",
                table: "Banks",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentTransactionCategoryId",
                table: "Categories",
                column: "ParentTransactionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TransactionTypeId",
                table: "Categories",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Imports_BankId",
                table: "Imports",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Imports_SourceAccountId",
                table: "Imports",
                column: "SourceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionImports_ImportId",
                table: "TransactionImports",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRules_TransactionRuleConditionId",
                table: "TransactionRules",
                column: "TransactionRuleConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRules_TransactionRuleFieldId",
                table: "TransactionRules",
                column: "TransactionRuleFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRules_TransactionRulesGroupId",
                table: "TransactionRules",
                column: "TransactionRulesGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRulesGroups_TransactionCategoryId",
                table: "TransactionRulesGroups",
                column: "TransactionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRulesGroups_TransactionRulesGroupOperatorId",
                table: "TransactionRulesGroups",
                column: "TransactionRulesGroupOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecurrencyTypeId",
                table: "Transactions",
                column: "RecurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceAccountId",
                table: "Transactions",
                column: "SourceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TargetAccountId",
                table: "Transactions",
                column: "TargetAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionCategoryId",
                table: "Transactions",
                column: "TransactionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionEntryTypeId",
                table: "Transactions",
                column: "TransactionEntryTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionImports");

            migrationBuilder.DropTable(
                name: "TransactionRules");

            migrationBuilder.DropTable(
                name: "Imports");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionRuleConditions");

            migrationBuilder.DropTable(
                name: "TransactionRuleFields");

            migrationBuilder.DropTable(
                name: "TransactionRulesGroups");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "RecurrencyTypes");

            migrationBuilder.DropTable(
                name: "TransactionEntryTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "TransactionRulesGroupOperator");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "TransactionTypes");
        }
    }
}
