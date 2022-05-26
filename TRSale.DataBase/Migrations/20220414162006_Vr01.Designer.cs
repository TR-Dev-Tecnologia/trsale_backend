﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TRSale.DataBase;

#nullable disable

namespace TRSale.DataBase.Migrations
{
    [DbContext(typeof(TRSaleContext))]
    [Migration("20220414162006_Vr01")]
    partial class Vr01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TRSale.Domain.Entites.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("LastAccess")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordToken")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("PasswordTokenValidity")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("UnqUserEmail");

                    b.ToTable("User", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "utf8");
                });
#pragma warning restore 612, 618
        }
    }
}