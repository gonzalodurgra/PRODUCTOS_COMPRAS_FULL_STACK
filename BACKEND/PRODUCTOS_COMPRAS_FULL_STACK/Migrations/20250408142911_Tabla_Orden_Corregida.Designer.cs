﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PRODUCTOS_COMPRAS_FULL_STACK.Models;

#nullable disable

namespace PRODUCTOS_COMPRAS_FULL_STACK.Migrations
{
    [DbContext(typeof(ProductoOrdenContext))]
    [Migration("20250408142911_Tabla_Orden_Corregida")]
    partial class Tabla_Orden_Corregida
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PRODUCTOS_COMPRAS_FULL_STACK.Models.Orden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TIMESTAMP");

                    b.Property<int?>("ProductoId")
                        .HasColumnType("integer");

                    b.Property<float>("precio")
                        .HasColumnType("FLOAT");

                    b.Property<int>("total")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("PRODUCTOS_COMPRAS_FULL_STACK.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)");

                    b.Property<int?>("OrdenId")
                        .HasColumnType("integer");

                    b.Property<float>("Precio")
                        .HasColumnType("FLOAT");

                    b.Property<int>("stock")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("OrdenId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("PRODUCTOS_COMPRAS_FULL_STACK.Models.Orden", b =>
                {
                    b.HasOne("PRODUCTOS_COMPRAS_FULL_STACK.Models.Producto", null)
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PRODUCTOS_COMPRAS_FULL_STACK.Models.Producto", b =>
                {
                    b.HasOne("PRODUCTOS_COMPRAS_FULL_STACK.Models.Orden", null)
                        .WithMany("productos")
                        .HasForeignKey("OrdenId");
                });

            modelBuilder.Entity("PRODUCTOS_COMPRAS_FULL_STACK.Models.Orden", b =>
                {
                    b.Navigation("productos");
                });
#pragma warning restore 612, 618
        }
    }
}
