﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using finefin.api.Data;

#nullable disable

namespace finefin.api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250215001324_DataModel")]
    partial class DataModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("finefin.api.Models.Entities.Recurrency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RCR_ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("RCR_CREATED_AT");

                    b.Property<int>("Occurrences")
                        .HasColumnType("int")
                        .HasColumnName("RCR_OCCURRENCES");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("RCR_TYPE");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("RCR_UPDATED_AT");

                    b.HasKey("Id");

                    b.ToTable("TB_RECURRENCY", (string)null);
                });

            modelBuilder.Entity("finefin.api.Models.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TRN_ID");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TRN_AMOUNT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("TRN_CREATED_AT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("TRN_DUEDATE");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit")
                        .HasColumnName("TRN_COMPLETED");

                    b.Property<bool>("IsFixed")
                        .HasColumnType("bit")
                        .HasColumnName("TRN_FIXED");

                    b.Property<bool>("IsRecurring")
                        .HasColumnType("bit")
                        .HasColumnName("TRN_RECURRING");

                    b.Property<Guid?>("RecurrencyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TRN_RECURRENCY_ID");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("TRN_TYPE");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("TRN_UPDATED_AT");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TRN_WALLET_ID");

                    b.HasKey("Id");

                    b.HasIndex("RecurrencyId");

                    b.HasIndex("WalletId");

                    b.ToTable("TB_TRANSACTION", (string)null);
                });

            modelBuilder.Entity("finefin.api.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("USR_ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("USR_CREATED_AT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("USR_EMAIL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("USR_NAME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USR_PASSWORD");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("USR_UPDATED_AT");

                    b.HasKey("Id");

                    b.ToTable("TB_USERS", (string)null);
                });

            modelBuilder.Entity("finefin.api.Models.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("WLT_ID");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("WLT_BALANCE");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("WLT_COLOR");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("WLT_CREATED_AT");

                    b.Property<DateTime>("LastEditDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("WLT_LAST_EDIT_DATE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("WLT_NAME");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("WLT_TYPE");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("WLT_UPDATED_AT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("WLT_USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TB_WALLET", (string)null);
                });

            modelBuilder.Entity("finefin.api.Models.Entities.Transaction", b =>
                {
                    b.HasOne("finefin.api.Models.Entities.Recurrency", "Recurrency")
                        .WithMany("Transactions")
                        .HasForeignKey("RecurrencyId");

                    b.HasOne("finefin.api.Models.Entities.Wallet", "Wallet")
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recurrency");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("finefin.api.Models.Entities.Wallet", b =>
                {
                    b.HasOne("finefin.api.Models.Entities.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("finefin.api.Models.Entities.Recurrency", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("finefin.api.Models.Entities.User", b =>
                {
                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("finefin.api.Models.Entities.Wallet", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
