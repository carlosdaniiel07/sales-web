﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalesWeb.Data;

namespace SalesWeb.Migrations
{
    [DbContext(typeof(SalesWebContext))]
    [Migration("20181027233551_IntegridadeReferencial")]
    partial class IntegridadeReferencial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SalesWeb.Models.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("SalesWeb.Models.Vendas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("Status");

                    b.Property<double>("Valor");

                    b.Property<int?>("VendedorId");

                    b.HasKey("Id");

                    b.HasIndex("VendedorId");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("SalesWeb.Models.Vendedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DepartamentoId");

                    b.Property<string>("Email");

                    b.Property<DateTime>("Nascimento");

                    b.Property<string>("Nome");

                    b.Property<double>("Salario");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Vendedor");
                });

            modelBuilder.Entity("SalesWeb.Models.Vendas", b =>
                {
                    b.HasOne("SalesWeb.Models.Vendedor", "Vendedor")
                        .WithMany("Vendas")
                        .HasForeignKey("VendedorId");
                });

            modelBuilder.Entity("SalesWeb.Models.Vendedor", b =>
                {
                    b.HasOne("SalesWeb.Models.Departamento", "Departamento")
                        .WithMany("Vendedores")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}