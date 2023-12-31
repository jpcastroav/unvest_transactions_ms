﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using unvest_transactions_ms.Models;

#nullable disable

namespace unvest_transactions_ms.Migrations
{
    [DbContext(typeof(TransactionsContext))]
    partial class TransactionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("unvest_transactions_ms.Models.Balance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario")
                        .HasAnnotation("Relational:JsonPropertyName", "id_usuario");

                    b.Property<decimal>("Valor")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal")
                        .HasColumnName("valor")
                        .HasAnnotation("Relational:JsonPropertyName", "valor");

                    b.HasKey("Id");

                    b.ToTable("balance");
                });

            modelBuilder.Entity("unvest_transactions_ms.Models.Operacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cantidad")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal")
                        .HasColumnName("cantidad")
                        .HasAnnotation("Relational:JsonPropertyName", "cantidad");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha")
                        .HasAnnotation("Relational:JsonPropertyName", "fecha");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario")
                        .HasAnnotation("Relational:JsonPropertyName", "id_usuario");

                    b.Property<int>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("tipo")
                        .HasAnnotation("Relational:JsonPropertyName", "tipo");

                    b.HasKey("Id");

                    b.ToTable("operacion");
                });

            modelBuilder.Entity("unvest_transactions_ms.Models.Transaccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cantidad")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal")
                        .HasColumnName("cantidad")
                        .HasAnnotation("Relational:JsonPropertyName", "cantidad");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha")
                        .HasAnnotation("Relational:JsonPropertyName", "fecha");

                    b.Property<string>("IdEmpresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("id_empresa")
                        .HasAnnotation("Relational:JsonPropertyName", "id_empresa");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario")
                        .HasAnnotation("Relational:JsonPropertyName", "id_usuario");

                    b.Property<int>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("tipo")
                        .HasAnnotation("Relational:JsonPropertyName", "tipo");

                    b.Property<decimal>("ValorAccion")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal")
                        .HasColumnName("valor_accion")
                        .HasAnnotation("Relational:JsonPropertyName", "valor_accion");

                    b.HasKey("Id");

                    b.ToTable("transaccion");
                });
#pragma warning restore 612, 618
        }
    }
}
