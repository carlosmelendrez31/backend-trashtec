﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using trashtec_api.Data;

#nullable disable

namespace trashtec_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("trashtec_api.Models.DispositivoModel", b =>
                {
                    b.Property<long>("IdDispositivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("idDispositivo");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdDispositivo"));

                    b.Property<long?>("IdUsuario")
                        .HasColumnType("bigint")
                        .HasColumnName("IdUsuario");

                    b.Property<int>("Llenado")
                        .HasColumnType("integer")
                        .HasColumnName("Llenado");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Nombre");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Tipo");

                    b.HasKey("IdDispositivo");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Dispositivos");
                });

            modelBuilder.Entity("trashtec_api.Models.UsuariosModel", b =>
                {
                    b.Property<long>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("idUsuario");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdUsuario"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Apellido");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contrasena");

                    b.Property<long?>("DispositivoId")
                        .HasColumnType("bigint")
                        .HasColumnName("Dispositivoid");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Nombre");

                    b.HasKey("IdUsuario");

                    b.HasIndex("DispositivoId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("trashtec_api.Models.DispositivoModel", b =>
                {
                    b.HasOne("trashtec_api.Models.UsuariosModel", null)
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("trashtec_api.Models.UsuariosModel", b =>
                {
                    b.HasOne("trashtec_api.Models.DispositivoModel", null)
                        .WithMany()
                        .HasForeignKey("DispositivoId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
