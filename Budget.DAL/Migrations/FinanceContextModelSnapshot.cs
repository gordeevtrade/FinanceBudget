﻿using FamilyBudjetAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Budget.DAL.Migrations
{
    [DbContext(typeof(FinanceContext))]
    internal partial class FinanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FamilyBudjetAPI.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryTypeId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryTypeId = 1,
                            Name = "Salary"
                        },
                        new
                        {
                            Id = 2,
                            CategoryTypeId = 1,
                            Name = "Stock"
                        },
                        new
                        {
                            Id = 3,
                            CategoryTypeId = 1,
                            Name = "Houses"
                        },
                        new
                        {
                            Id = 4,
                            CategoryTypeId = 2,
                            Name = "Groceries"
                        },
                        new
                        {
                            Id = 5,
                            CategoryTypeId = 2,
                            Name = "Собаки"
                        });
                });

            modelBuilder.Entity("FamilyBudjetAPI.CategoryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Income"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Expense"
                        });
                });

            modelBuilder.Entity("FamilyBudjetAPI.FinanceTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("FinacneTransactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 1000m,
                            CategoryId = 1,
                            Date = new DateTime(2023, 6, 2, 12, 50, 9, 535, DateTimeKind.Local).AddTicks(432),
                            Note = "Salary for May"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 950m,
                            CategoryId = 2,
                            Date = new DateTime(2023, 6, 2, 12, 50, 9, 535, DateTimeKind.Local).AddTicks(476),
                            Note = "Microsoft"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 1850m,
                            CategoryId = 3,
                            Date = new DateTime(2023, 6, 2, 12, 50, 9, 535, DateTimeKind.Local).AddTicks(482),
                            Note = "Avalon"
                        },
                        new
                        {
                            Id = 4,
                            Amount = -200m,
                            CategoryId = 4,
                            Date = new DateTime(2023, 6, 2, 12, 50, 9, 535, DateTimeKind.Local).AddTicks(488),
                            Note = "Grocery shopping"
                        },
                        new
                        {
                            Id = 5,
                            Amount = -8700m,
                            CategoryId = 5,
                            Date = new DateTime(2023, 6, 2, 12, 50, 9, 535, DateTimeKind.Local).AddTicks(493),
                            Note = "Собаки "
                        });
                });

            modelBuilder.Entity("FamilyBudjetAPI.Category", b =>
                {
                    b.HasOne("FamilyBudjetAPI.CategoryType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("CategoryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("FamilyBudjetAPI.FinanceTransaction", b =>
                {
                    b.HasOne("FamilyBudjetAPI.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}