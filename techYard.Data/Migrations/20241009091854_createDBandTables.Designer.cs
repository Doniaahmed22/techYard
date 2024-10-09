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
    [Migration("20241009091854_createDBandTables")]
    partial class createDBandTables
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

                    b.HasKey("Id");

                    b.ToTable("productDetailsImages");
                });

            modelBuilder.Entity("techYard.Data.Entities.ProductFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dimensions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("graphicCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("model")
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

                    b.Property<double?>("NewPrice")
                        .HasColumnType("float");

                    b.Property<int?>("categoryId")
                        .HasColumnType("int");

                    b.Property<int?>("discount")
                        .HasColumnType("int");

                    b.Property<string>("imageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageUrlInHover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("oldPrice")
                        .HasColumnType("float");

                    b.Property<bool?>("popular")
                        .HasColumnType("bit");

                    b.Property<int?>("productDetailsImagesId")
                        .HasColumnType("int");

                    b.Property<int?>("productFeaturesId")
                        .HasColumnType("int");

                    b.Property<bool?>("soldOut")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("categoryId");

                    b.HasIndex("productDetailsImagesId");

                    b.HasIndex("productFeaturesId")
                        .IsUnique()
                        .HasFilter("[productFeaturesId] IS NOT NULL");

                    b.ToTable("products");
                });

            modelBuilder.Entity("techYard.Data.Entities.Products", b =>
                {
                    b.HasOne("techYard.Data.Entities.Categories", "category")
                        .WithMany("products")
                        .HasForeignKey("categoryId");

                    b.HasOne("techYard.Data.Entities.ProductDetailsImages", "productDetailsImages")
                        .WithMany("products")
                        .HasForeignKey("productDetailsImagesId");

                    b.HasOne("techYard.Data.Entities.ProductFeatures", "productFeatures")
                        .WithOne("products")
                        .HasForeignKey("techYard.Data.Entities.Products", "productFeaturesId");

                    b.Navigation("category");

                    b.Navigation("productDetailsImages");

                    b.Navigation("productFeatures");
                });

            modelBuilder.Entity("techYard.Data.Entities.Categories", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("techYard.Data.Entities.ProductDetailsImages", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("techYard.Data.Entities.ProductFeatures", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
