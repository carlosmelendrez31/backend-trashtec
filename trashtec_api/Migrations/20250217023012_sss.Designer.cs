﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using trashtec_api.Data;

#nullable disable

namespace trashtec_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250217023012_sss")]
    partial class sss
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("trashtec_api.Models.DispositivoModel", b =>
                {
                    b.Property<int>("IdDispositivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdDispositivo"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("IdDispositivo");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Dispositivos");
                });

            modelBuilder.Entity("trashtec_api.Models.UsuariosModel", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("trashtec_api.Models.DispositivoModel", b =>
                {
                    b.HasOne("trashtec_api.Models.UsuariosModel", "Usuario")
                        .WithMany("Dispositivos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("trashtec_api.Models.UsuariosModel", b =>
                {
                    b.Navigation("Dispositivos");
                });
#pragma warning restore 612, 618
        }
    }
}
