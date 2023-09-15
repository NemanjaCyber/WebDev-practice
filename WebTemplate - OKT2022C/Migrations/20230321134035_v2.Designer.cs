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
    [Migration("20230321134035_v2")]
    partial class v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Radnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ImePrezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IspraznioKolicinu")
                        .HasColumnType("int");

                    b.Property<int?>("SilosID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SilosID");

                    b.ToTable("Radnici");
                });

            modelBuilder.Entity("Models.Silos", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Kapacitet")
                        .HasColumnType("int");

                    b.Property<string>("Oznaka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrenutnaPopunjenost")
                        .HasColumnType("int");

                    b.Property<int>("Vlaznost")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Silosi");
                });

            modelBuilder.Entity("Models.Radnik", b =>
                {
                    b.HasOne("Models.Silos", "Silos")
                        .WithMany("Radnici")
                        .HasForeignKey("SilosID");

                    b.Navigation("Silos");
                });

            modelBuilder.Entity("Models.Silos", b =>
                {
                    b.Navigation("Radnici");
                });
#pragma warning restore 612, 618
        }
    }
}