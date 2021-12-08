using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FacturacionAPI.Models
{
    public partial class FacturacionContext : DbContext
    {
        public FacturacionContext()
        {
        }

        public FacturacionContext(DbContextOptions<FacturacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulos> Articulos { get; set; }
        public virtual DbSet<AsientosContables> AsientosContables { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Facturacion> Facturacion { get; set; }
        public virtual DbSet<VFacturacion> VFacturacion { get; set; }
        public virtual DbSet<Vendedores> Vendedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:unapec-iso810-815.database.windows.net,1433;Initial Catalog=Facturacion;Persist Security Info=False;User ID=facturacion;Password=Bobote1@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulos>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<AsientosContables>(entity =>
            {
                entity.Property(e => e.Cuenta)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAsiento).HasColumnType("datetime");

                entity.Property(e => e.MontoAsiento).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TipoMovimiento)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.AsientosContables)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__AsientosC__IdCli__6754599E");
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.Property(e => e.CuentaContable)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rnc)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Facturacion>(entity =>
            {
                entity.Property(e => e.Comentario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.Facturacion)
                    .HasForeignKey(d => d.IdArticulo)
                    .HasConstraintName("FK__Facturaci__IdArt__6477ECF3");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Facturacion)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Facturaci__IdCli__6383C8BA");

                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.Facturacion)
                    .HasForeignKey(d => d.IdVendedor)
                    .HasConstraintName("FK__Facturaci__IdVen__628FA481");
            });

            modelBuilder.Entity<VFacturacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vFacturacion");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Monto).HasColumnType("decimal(29, 2)");
            });

            modelBuilder.Entity<Vendedores>(entity =>
            {
                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Comision).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
