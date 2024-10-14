﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using techYard.Data.Context;

#nullable disable

namespace techYard.Data.Migrations
{
    [DbContext(typeof(techYardDbContext))]
    [Migration("20241014123918_RecreateProducts")]
    partial class RecreateProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("techYard.Data.Entities.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("imageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("techYard.Data.Entities.ProductDetailsImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("productsId");

                    b.ToTable("productDetailsImages");
                });

            modelBuilder.Entity("techYard.Data.Entities.ProductFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ProductsId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("ScreenSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dimensions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("graphicCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("processor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ramSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ramType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("storage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("weight")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductsId");

                    b.ToTable("productFeatures");
                });

            modelBuilder.Entity("techYard.Data.Entities.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("categoriesId")
                        .HasColumnType("int");

                    b.Property<int?>("discount")
                        .HasColumnType("int");

                    b.Property<string>("imageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageUrlInHover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("oldPrice")
                        .HasColumnType("float");

                    b.Property<bool?>("popular")
                        .HasColumnType("bit");

                    b.Property<bool?>("soldOut")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("categoriesId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("techYard.Data.Entities.ProductDetailsImages", b =>
                {
                    b.HasOne("techYard.Data.Entities.Products", "product")
                        .WithMany("productDetailsImages")
                        .HasForeignKey("productsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("techYard.Data.Entities.ProductFeatures", b =>
                {
                    b.HasOne("techYard.Data.Entities.Products", "Product")
                        .WithMany("ProductFeatures")
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("techYard.Data.Entities.Products", b =>
                {
                    b.HasOne("techYard.Data.Entities.Categories", "categories")
                        .WithMany("products")
                        .HasForeignKey("categoriesId");

                    b.Navigation("categories");
                });

            modelBuilder.Entity("techYard.Data.Entities.Categories", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("techYard.Data.Entities.Products", b =>
                {
                    b.Navigation("ProductFeatures");

                    b.Navigation("productDetailsImages");
                });
#pragma warning restore 612, 618
        }
    }
}
