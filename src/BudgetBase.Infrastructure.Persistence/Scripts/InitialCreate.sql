CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Accounts" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Number" text NOT NULL,
    "IBAN" text NOT NULL,
    "CreatedOn" timestamp with time zone NOT NULL,
    "CreatedBy" text NOT NULL,
    "ModifiedOn" timestamp with time zone NOT NULL,
    "ModifiedBy" text NOT NULL,
    CONSTRAINT "PK_Accounts" PRIMARY KEY ("Id")
);

CREATE TABLE "Countries" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    CONSTRAINT "PK_Countries" PRIMARY KEY ("Id")
);

CREATE TABLE "RecurrencyTypes" (
    "Id" uuid NOT NULL,
    "Index" integer NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_RecurrencyTypes" PRIMARY KEY ("Id")
);

CREATE TABLE "TransactionEntryTypes" (
    "Id" uuid NOT NULL,
    "Index" integer NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_TransactionEntryTypes" PRIMARY KEY ("Id")
);

CREATE TABLE "TransactionRuleConditions" (
    "Id" uuid NOT NULL,
    "Index" integer NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_TransactionRuleConditions" PRIMARY KEY ("Id")
);

CREATE TABLE "TransactionRuleFields" (
    "Id" uuid NOT NULL,
    "Index" integer NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_TransactionRuleFields" PRIMARY KEY ("Id")
);

CREATE TABLE "TransactionRulesGroupOperator" (
    "Id" uuid NOT NULL,
    "Index" integer NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_TransactionRulesGroupOperator" PRIMARY KEY ("Id")
);

CREATE TABLE "TransactionTypes" (
    "Id" uuid NOT NULL,
    "Index" integer NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_TransactionTypes" PRIMARY KEY ("Id")
);

CREATE TABLE "Banks" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "CountryId" uuid NOT NULL,
    CONSTRAINT "PK_Banks" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Banks_Countries_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Countries" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Categories" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "Description" text NULL,
    "Icon" text NULL,
    "Color" text NULL,
    "TransactionTypeId" uuid NULL,
    "ParentTransactionCategoryId" uuid NULL,
    "CreatedOn" timestamp with time zone NOT NULL,
    "CreatedBy" text NOT NULL,
    "ModifiedOn" timestamp with time zone NOT NULL,
    "ModifiedBy" text NOT NULL,
    CONSTRAINT "PK_Categories" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Categories_Categories_ParentTransactionCategoryId" FOREIGN KEY ("ParentTransactionCategoryId") REFERENCES "Categories" ("Id"),
    CONSTRAINT "FK_Categories_TransactionTypes_TransactionTypeId" FOREIGN KEY ("TransactionTypeId") REFERENCES "TransactionTypes" ("Id")
);

CREATE TABLE "Imports" (
    "Id" uuid NOT NULL,
    "Date" timestamp with time zone NOT NULL,
    "InsertDuplicates" boolean NOT NULL,
    "IgnoreRules" boolean NOT NULL,
    "Filename" text NOT NULL,
    "TransactionsCount" integer NOT NULL,
    "TransactionsInserted" integer NOT NULL,
    "BankId" uuid NULL,
    "SourceAccountId" uuid NULL,
    "CreatedOn" timestamp with time zone NOT NULL,
    "CreatedBy" text NOT NULL,
    "ModifiedOn" timestamp with time zone NOT NULL,
    "ModifiedBy" text NOT NULL,
    CONSTRAINT "PK_Imports" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Imports_Accounts_SourceAccountId" FOREIGN KEY ("SourceAccountId") REFERENCES "Accounts" ("Id"),
    CONSTRAINT "FK_Imports_Banks_BankId" FOREIGN KEY ("BankId") REFERENCES "Banks" ("Id")
);

CREATE TABLE "TransactionRulesGroups" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "TransactionCategoryId" uuid NOT NULL,
    "TransactionRulesGroupOperatorId" uuid NOT NULL,
    "CreatedOn" timestamp with time zone NOT NULL,
    "CreatedBy" text NOT NULL,
    "ModifiedOn" timestamp with time zone NOT NULL,
    "ModifiedBy" text NOT NULL,
    CONSTRAINT "PK_TransactionRulesGroups" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TransactionRulesGroups_Categories_TransactionCategoryId" FOREIGN KEY ("TransactionCategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TransactionRulesGroups_TransactionRulesGroupOperator_Transa~" FOREIGN KEY ("TransactionRulesGroupOperatorId") REFERENCES "TransactionRulesGroupOperator" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Transactions" (
    "Id" uuid NOT NULL,
    "Description" text NOT NULL,
    "Date" timestamp with time zone NOT NULL,
    "Amount" numeric NOT NULL,
    "SourceAccountId" uuid NOT NULL,
    "TargetAccountId" uuid NULL,
    "TransactionEntryTypeId" uuid NULL,
    "RecurrencyTypeId" uuid NULL,
    "TransactionCategoryId" uuid NULL,
    "CreatedOn" timestamp with time zone NOT NULL,
    "CreatedBy" text NOT NULL,
    "ModifiedOn" timestamp with time zone NOT NULL,
    "ModifiedBy" text NOT NULL,
    CONSTRAINT "PK_Transactions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Transactions_Accounts_SourceAccountId" FOREIGN KEY ("SourceAccountId") REFERENCES "Accounts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Transactions_Accounts_TargetAccountId" FOREIGN KEY ("TargetAccountId") REFERENCES "Accounts" ("Id"),
    CONSTRAINT "FK_Transactions_Categories_TransactionCategoryId" FOREIGN KEY ("TransactionCategoryId") REFERENCES "Categories" ("Id"),
    CONSTRAINT "FK_Transactions_RecurrencyTypes_RecurrencyTypeId" FOREIGN KEY ("RecurrencyTypeId") REFERENCES "RecurrencyTypes" ("Id"),
    CONSTRAINT "FK_Transactions_TransactionEntryTypes_TransactionEntryTypeId" FOREIGN KEY ("TransactionEntryTypeId") REFERENCES "TransactionEntryTypes" ("Id")
);

CREATE TABLE "TransactionRules" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Value" text NOT NULL,
    "TransactionRuleFieldId" uuid NOT NULL,
    "TransactionRuleConditionId" uuid NOT NULL,
    "TransactionRulesGroupId" uuid NOT NULL,
    "CreatedOn" timestamp with time zone NOT NULL,
    "CreatedBy" text NOT NULL,
    "ModifiedOn" timestamp with time zone NOT NULL,
    "ModifiedBy" text NOT NULL,
    CONSTRAINT "PK_TransactionRules" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TransactionRules_TransactionRuleConditions_TransactionRuleC~" FOREIGN KEY ("TransactionRuleConditionId") REFERENCES "TransactionRuleConditions" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TransactionRules_TransactionRuleFields_TransactionRuleField~" FOREIGN KEY ("TransactionRuleFieldId") REFERENCES "TransactionRuleFields" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TransactionRules_TransactionRulesGroups_TransactionRulesGro~" FOREIGN KEY ("TransactionRulesGroupId") REFERENCES "TransactionRulesGroups" ("Id") ON DELETE CASCADE
);

CREATE TABLE "TransactionImports" (
    "TransactionId" uuid NOT NULL,
    "ImportId" uuid NOT NULL,
    CONSTRAINT "PK_TransactionImports" PRIMARY KEY ("TransactionId", "ImportId"),
    CONSTRAINT "FK_TransactionImports_Imports_ImportId" FOREIGN KEY ("ImportId") REFERENCES "Imports" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TransactionImports_Transactions_TransactionId" FOREIGN KEY ("TransactionId") REFERENCES "Transactions" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Banks_CountryId" ON "Banks" ("CountryId");

CREATE INDEX "IX_Categories_ParentTransactionCategoryId" ON "Categories" ("ParentTransactionCategoryId");

CREATE INDEX "IX_Categories_TransactionTypeId" ON "Categories" ("TransactionTypeId");

CREATE INDEX "IX_Imports_BankId" ON "Imports" ("BankId");

CREATE INDEX "IX_Imports_SourceAccountId" ON "Imports" ("SourceAccountId");

CREATE INDEX "IX_TransactionImports_ImportId" ON "TransactionImports" ("ImportId");

CREATE INDEX "IX_TransactionRules_TransactionRuleConditionId" ON "TransactionRules" ("TransactionRuleConditionId");

CREATE INDEX "IX_TransactionRules_TransactionRuleFieldId" ON "TransactionRules" ("TransactionRuleFieldId");

CREATE INDEX "IX_TransactionRules_TransactionRulesGroupId" ON "TransactionRules" ("TransactionRulesGroupId");

CREATE INDEX "IX_TransactionRulesGroups_TransactionCategoryId" ON "TransactionRulesGroups" ("TransactionCategoryId");

CREATE INDEX "IX_TransactionRulesGroups_TransactionRulesGroupOperatorId" ON "TransactionRulesGroups" ("TransactionRulesGroupOperatorId");

CREATE INDEX "IX_Transactions_RecurrencyTypeId" ON "Transactions" ("RecurrencyTypeId");

CREATE INDEX "IX_Transactions_SourceAccountId" ON "Transactions" ("SourceAccountId");

CREATE INDEX "IX_Transactions_TargetAccountId" ON "Transactions" ("TargetAccountId");

CREATE INDEX "IX_Transactions_TransactionCategoryId" ON "Transactions" ("TransactionCategoryId");

CREATE INDEX "IX_Transactions_TransactionEntryTypeId" ON "Transactions" ("TransactionEntryTypeId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240918223314_InitialCreate', '7.0.5');

COMMIT;

