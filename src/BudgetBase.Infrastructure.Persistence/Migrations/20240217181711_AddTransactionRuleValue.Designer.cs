﻿// <auto-generated />
using System;
using BudgetBase.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240217181711_AddTransactionRuleValue")]
    partial class AddTransactionRuleValue
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IBAN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Import", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BankId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("InsertDuplicates")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("SourceAccountId")
                        .HasColumnType("uuid");

                    b.Property<int>("TransactionsCount")
                        .HasColumnType("integer");

                    b.Property<int>("TransactionsInserted")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("SourceAccountId");

                    b.ToTable("Imports");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.RecurrencyType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RecurrencyTypes");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("RecurrencyTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SourceAccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TargetAccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TransactionCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TransactionEntryTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RecurrencyTypeId");

                    b.HasIndex("SourceAccountId");

                    b.HasIndex("TargetAccountId");

                    b.HasIndex("TransactionCategoryId");

                    b.HasIndex("TransactionEntryTypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParentTransactionCategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TransactionTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentTransactionCategoryId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionEntryType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TransactionEntryTypes");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionImport", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ImportId")
                        .HasColumnType("uuid");

                    b.HasKey("TransactionId", "ImportId");

                    b.HasIndex("ImportId");

                    b.ToTable("TransactionImports");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TransactionRuleConditionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TransactionRuleFieldId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TransactionRuleConditionId");

                    b.HasIndex("TransactionRuleFieldId");

                    b.ToTable("TransactionRules");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionRuleCondition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TransactionRuleConditions");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionRuleField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TransactionRuleFields");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Bank", b =>
                {
                    b.HasOne("BudgetBase.Core.Domain.Entities.Country", "Country")
                        .WithMany("Banks")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Import", b =>
                {
                    b.HasOne("BudgetBase.Core.Domain.Entities.Bank", "Bank")
                        .WithMany("Imports")
                        .HasForeignKey("BankId");

                    b.HasOne("BudgetBase.Core.Domain.Entities.Account", "SourceAccount")
                        .WithMany("SourceAccountImports")
                        .HasForeignKey("SourceAccountId");

                    b.Navigation("Bank");

                    b.Navigation("SourceAccount");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("BudgetBase.Core.Domain.Entities.RecurrencyType", "RecurrencyType")
                        .WithMany("Transactions")
                        .HasForeignKey("RecurrencyTypeId");

                    b.HasOne("BudgetBase.Core.Domain.Entities.Account", "SourceAccount")
                        .WithMany("SourceAccountTransactions")
                        .HasForeignKey("SourceAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetBase.Core.Domain.Entities.Account", "TargetAccount")
                        .WithMany("TargetAccountTransactions")
                        .HasForeignKey("TargetAccountId");

                    b.HasOne("BudgetBase.Core.Domain.Entities.TransactionCategory", "TransactionCategory")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionCategoryId");

                    b.HasOne("BudgetBase.Core.Domain.Entities.TransactionEntryType", "TransactionEntryType")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionEntryTypeId");

                    b.Navigation("RecurrencyType");

                    b.Navigation("SourceAccount");

                    b.Navigation("TargetAccount");

                    b.Navigation("TransactionCategory");

                    b.Navigation("TransactionEntryType");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionCategory", b =>
                {
                    b.HasOne("BudgetBase.Core.Domain.Entities.TransactionCategory", "ParentTransactionCategory")
                        .WithMany("ChildrenTransactionCategory")
                        .HasForeignKey("ParentTransactionCategoryId");

                    b.HasOne("BudgetBase.Core.Domain.Entities.TransactionType", "TransactionType")
                        .WithMany("Categories")
                        .HasForeignKey("TransactionTypeId");

                    b.Navigation("ParentTransactionCategory");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionImport", b =>
                {
                    b.HasOne("BudgetBase.Core.Domain.Entities.Import", "Import")
                        .WithMany("TransactionImports")
                        .HasForeignKey("ImportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetBase.Core.Domain.Entities.Transaction", "Transaction")
                        .WithMany("TransactionImports")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Import");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionRule", b =>
                {
                    b.HasOne("BudgetBase.Core.Domain.Entities.TransactionRuleCondition", "TransactionRuleCondition")
                        .WithMany()
                        .HasForeignKey("TransactionRuleConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetBase.Core.Domain.Entities.TransactionRuleField", "TransactionRuleField")
                        .WithMany()
                        .HasForeignKey("TransactionRuleFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionRuleCondition");

                    b.Navigation("TransactionRuleField");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Account", b =>
                {
                    b.Navigation("SourceAccountImports");

                    b.Navigation("SourceAccountTransactions");

                    b.Navigation("TargetAccountTransactions");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Bank", b =>
                {
                    b.Navigation("Imports");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Country", b =>
                {
                    b.Navigation("Banks");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Import", b =>
                {
                    b.Navigation("TransactionImports");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.RecurrencyType", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.Transaction", b =>
                {
                    b.Navigation("TransactionImports");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionCategory", b =>
                {
                    b.Navigation("ChildrenTransactionCategory");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionEntryType", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BudgetBase.Core.Domain.Entities.TransactionType", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
