﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoService.AccesoDatos;

#nullable disable

namespace ProyectoService.AccesoDatos.Migrations
{
    [DbContext(typeof(ProyectoServiceContext))]
    partial class ProyectoServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("UsuarioSequence");

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Mensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DestinatarioId")
                        .HasColumnType("int");

                    b.Property<int>("EmisorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaHoraEnvio")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReparacionId")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DestinatarioId");

                    b.HasIndex("EmisorId");

                    b.HasIndex("ReparacionId");

                    b.ToTable("Mensajes");
                });

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Reparacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<double>("CostoFinal")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescripcionPresupuesto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPresupuesto")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPromesaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPromesaPresupuesto")
                        .HasColumnType("datetime2");

                    b.Property<double>("ManoDeObra")
                        .HasColumnType("float");

                    b.Property<string>("NumeroSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazonNoAceptada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Reparada")
                        .HasColumnType("bit");

                    b.Property<int>("TecnicoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("TecnicoId");

                    b.ToTable("Reparacion");
                });

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [UsuarioSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Administrador", b =>
                {
                    b.HasBaseType("ProyectoService.LogicaNegocio.Modelo.Usuario");

                    b.ToTable("Administrador", (string)null);
                });

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Cliente", b =>
                {
                    b.HasBaseType("ProyectoService.LogicaNegocio.Modelo.Usuario");

                    b.Property<string>("Ci")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Tecnico", b =>
                {
                    b.HasBaseType("ProyectoService.LogicaNegocio.Modelo.Usuario");

                    b.ToTable("Tecnico", (string)null);
                });

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Mensaje", b =>
                {
                    b.HasOne("ProyectoService.LogicaNegocio.Modelo.Usuario", "Destinatario")
                        .WithMany()
                        .HasForeignKey("DestinatarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProyectoService.LogicaNegocio.Modelo.Usuario", "Emisor")
                        .WithMany()
                        .HasForeignKey("EmisorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProyectoService.LogicaNegocio.Modelo.Reparacion", "Reparacion")
                        .WithMany("Mensajes")
                        .HasForeignKey("ReparacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destinatario");

                    b.Navigation("Emisor");

                    b.Navigation("Reparacion");
                });

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Reparacion", b =>
                {
                    b.HasOne("ProyectoService.LogicaNegocio.Modelo.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoService.LogicaNegocio.Modelo.Tecnico", "Tecnico")
                        .WithMany()
                        .HasForeignKey("TecnicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Tecnico");
                });

            modelBuilder.Entity("ProyectoService.LogicaNegocio.Modelo.Reparacion", b =>
                {
                    b.Navigation("Mensajes");
                });
#pragma warning restore 612, 618
        }
    }
}
