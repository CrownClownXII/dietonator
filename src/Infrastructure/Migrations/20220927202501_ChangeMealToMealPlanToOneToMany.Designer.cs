﻿// <auto-generated />
using System;
using Dietonator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dietonator.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220927202501_ChangeMealToMealPlanToOneToMany")]
    partial class ChangeMealToMealPlanToOneToMany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dietonator.Domain.Entities.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<Guid?>("MealPlanId")
                        .HasColumnType("uuid")
                        .HasColumnName("mealplanid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_meals");

                    b.HasIndex("MealPlanId")
                        .HasDatabaseName("ix_meals_mealplanid");

                    b.ToTable("meals", (string)null);
                });

            modelBuilder.Entity("Dietonator.Domain.Entities.MealPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateOnly>("ForDate")
                        .HasColumnType("date")
                        .HasColumnName("fordate");

                    b.Property<Guid>("ForUser")
                        .HasColumnType("uuid")
                        .HasColumnName("foruser");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.HasKey("Id")
                        .HasName("pk_mealplans");

                    b.ToTable("mealplans", (string)null);
                });

            modelBuilder.Entity("Dietonator.Domain.Entities.MealProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Amount")
                        .HasColumnType("integer")
                        .HasColumnName("amount");

                    b.Property<int>("AmountType")
                        .HasColumnType("integer")
                        .HasColumnName("amounttype");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<Guid>("MealId")
                        .HasColumnType("uuid")
                        .HasColumnName("mealid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("productid");

                    b.HasKey("Id")
                        .HasName("pk_mealproducts");

                    b.HasIndex("MealId")
                        .HasDatabaseName("ix_mealproducts_mealid");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_mealproducts_productid");

                    b.ToTable("mealproducts", (string)null);
                });

            modelBuilder.Entity("Dietonator.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<float>("Carbohydrates")
                        .HasColumnType("real")
                        .HasColumnName("carbohydrates");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<float>("Fats")
                        .HasColumnType("real")
                        .HasColumnName("fats");

                    b.Property<int>("Kcal")
                        .HasColumnType("integer")
                        .HasColumnName("kcal");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<float>("Proteins")
                        .HasColumnType("real")
                        .HasColumnName("proteins");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("Dietonator.Domain.Entities.Meal", b =>
                {
                    b.HasOne("Dietonator.Domain.Entities.MealPlan", null)
                        .WithMany("Meals")
                        .HasForeignKey("MealPlanId")
                        .HasConstraintName("fk_meals_mealplans_mealplanid");
                });

            modelBuilder.Entity("Dietonator.Domain.Entities.MealProduct", b =>
                {
                    b.HasOne("Dietonator.Domain.Entities.Meal", null)
                        .WithMany("Products")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_mealproducts_meals_mealid");

                    b.HasOne("Dietonator.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_mealproducts_products_productid");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Dietonator.Domain.Entities.Meal", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Dietonator.Domain.Entities.MealPlan", b =>
                {
                    b.Navigation("Meals");
                });
#pragma warning restore 612, 618
        }
    }
}
