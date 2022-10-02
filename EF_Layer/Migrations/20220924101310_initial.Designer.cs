﻿// <auto-generated />
using System;
using BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessLogicLayer.Migrations
{
    [DbContext(typeof(MultiShopContext))]
    [Migration("20220924101310_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoreLayer.Entities.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CoreLayer.Entities.Colors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("CoreLayer.Entities.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProductCaregoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductColorId")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<byte[]>("productLastUpdate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("productSize")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("productStock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductCaregoryId");

                    b.HasIndex("ProductColorId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CoreLayer.Entities.Products", b =>
                {
                    b.HasOne("CoreLayer.Entities.Categories", "ProductCaregory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCaregoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Products_Categories");

                    b.HasOne("CoreLayer.Entities.Colors", "ProductColor")
                        .WithMany("Products")
                        .HasForeignKey("ProductColorId")
                        .IsRequired()
                        .HasConstraintName("FK_Products_Colors");

                    b.Navigation("ProductCaregory");

                    b.Navigation("ProductColor");
                });

            modelBuilder.Entity("CoreLayer.Entities.Categories", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CoreLayer.Entities.Colors", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}