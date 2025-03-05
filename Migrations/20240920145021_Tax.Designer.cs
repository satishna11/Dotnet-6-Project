﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using inventory_re.DAO;

#nullable disable

namespace inventory_re.Migrations
{
    [DbContext(typeof(appDbContext))]
    [Migration("20240920145021_Tax")]
    partial class Tax
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("inventory_re.Model.Unit", b =>
                {
                    b.Property<int>("UnitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitID"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("UnitCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitID");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("inventory_re.Model.UserGroup", b =>
                {
                    b.Property<int>("UserGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserGroupID"), 1L, 1);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserGroupCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserGroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserGroupID");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("inventory_re.Model.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserGroupID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("UserID");

                    b.HasIndex("UserGroupID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("inventory_re.Models.Tax", b =>
                {
                    b.Property<int>("TaxID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaxID"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.Property<string>("TaxCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaxID");

                    b.ToTable("Tax");
                });

            modelBuilder.Entity("inventory_re.Model.Users", b =>
                {
                    b.HasOne("inventory_re.Model.UserGroup", "UserGroup")
                        .WithMany()
                        .HasForeignKey("UserGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
