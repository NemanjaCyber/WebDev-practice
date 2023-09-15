﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace WebTemplate.Migrations
{
    [DbContext(typeof(IspitContext))]
    [Migration("20230322140222_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Zarada")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Prodavnice");
                });

            modelBuilder.Entity("Models.Sastojak", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProdavnicaID")
                        .HasColumnType("int");

                    b.Property<int?>("StoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProdavnicaID");

                    b.HasIndex("StoID");

                    b.ToTable("Sastojci");
                });

            modelBuilder.Entity("Models.Sto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("KolicinaSastojaka")
                        .HasColumnType("int");

                    b.Property<int?>("MojaProdavnicaID")
                        .HasColumnType("int");

                    b.Property<string>("Oznaka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VrednostStola")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MojaProdavnicaID");

                    b.ToTable("Stolovi");
                });

            modelBuilder.Entity("Models.Sastojak", b =>
                {
                    b.HasOne("Models.Prodavnica", "Prodavnica")
                        .WithMany("Sastojci")
                        .HasForeignKey("ProdavnicaID");

                    b.HasOne("Models.Sto", "Sto")
                        .WithMany("SastojciNaStolu")
                        .HasForeignKey("StoID");

                    b.Navigation("Prodavnica");

                    b.Navigation("Sto");
                });

            modelBuilder.Entity("Models.Sto", b =>
                {
                    b.HasOne("Models.Prodavnica", "MojaProdavnica")
                        .WithMany("Stolovi")
                        .HasForeignKey("MojaProdavnicaID");

                    b.Navigation("MojaProdavnica");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Navigation("Sastojci");

                    b.Navigation("Stolovi");
                });

            modelBuilder.Entity("Models.Sto", b =>
                {
                    b.Navigation("SastojciNaStolu");
                });
#pragma warning restore 612, 618
        }
    }
}