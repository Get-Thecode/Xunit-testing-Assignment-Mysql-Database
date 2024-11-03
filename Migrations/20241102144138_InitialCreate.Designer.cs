﻿// <auto-generated />
using System;
using ClaimsManagementApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClaimsManagementApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241102144138_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ClaimsManagementApi.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ClaimAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DateOfClaim")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FilePath")
                        .HasColumnType("longtext");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasDefaultValue("Pending");

                    b.HasKey("Id");

                    b.ToTable("Claims");
                });
#pragma warning restore 612, 618
        }
    }
}
