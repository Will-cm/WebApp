using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApp.Models;

namespace WebApp.Data.Context
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<almacen> almacen { get; set; } = null!;
        public virtual DbSet<categoria> categoria { get; set; } = null!;
        public virtual DbSet<corte> corte { get; set; } = null!;
        public virtual DbSet<corte_almacen> corte_almacen { get; set; } = null!;
        public virtual DbSet<detalle_ingreso> detalle_ingreso { get; set; } = null!;
        public virtual DbSet<detalle_salida> detalle_salida { get; set; } = null!;
        public virtual DbSet<inventario> inventario { get; set; } = null!;
        public virtual DbSet<item> item { get; set; } = null!;
        public virtual DbSet<modulo> modulo { get; set; } = null!;
        public virtual DbSet<nota_ingreso> nota_ingreso { get; set; } = null!;
        public virtual DbSet<nota_salida> nota_salida { get; set; } = null!;
        public virtual DbSet<operaciones> operaciones { get; set; } = null!;
        public virtual DbSet<picking_nsalida> picking_nsalida { get; set; } = null!;
        public virtual DbSet<rack> rack { get; set; } = null!;
        public virtual DbSet<rol> rol { get; set; } = null!;
        public virtual DbSet<subcategoria> subcategoria { get; set; } = null!;
        public virtual DbSet<sucursal> sucursal { get; set; } = null!;
        public virtual DbSet<users> users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<almacen>(entity =>
            {
                entity.HasIndex(e => e.id_item, "id_item");

                entity.HasIndex(e => e.id_rack, "id_rack");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cantidad)
                    .HasPrecision(12, 2)
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_item).HasColumnType("int(11)");

                entity.Property(e => e.id_rack).HasColumnType("int(11)");

                entity.Property(e => e.lote).HasMaxLength(20);

                entity.Property(e => e.observacion).HasMaxLength(50);

                entity.Property(e => e.serie).HasMaxLength(20);

                entity.Property(e => e.ubicacion).HasMaxLength(11);

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<categoria>(entity =>
            {
                entity.HasIndex(e => e.codigo, "codigo")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.codigo).HasMaxLength(15);

                entity.Property(e => e.descripcion).HasMaxLength(80);

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<corte>(entity =>
            {
                entity.HasIndex(e => e.id_sucursal, "id_sucursal");

                entity.HasIndex(e => e.nro, "nro")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_sucursal).HasColumnType("int(11)");

                entity.Property(e => e.nro).HasColumnType("int(11)");

                entity.Property(e => e.tip_inventario).HasColumnType("smallint(6)");
            });

            modelBuilder.Entity<corte_almacen>(entity =>
            {
                entity.HasIndex(e => e.id_corte, "id_corte");

                entity.HasIndex(e => e.id_item, "id_item");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cantidad)
                    .HasPrecision(12, 2)
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_corte).HasColumnType("int(11)");

                entity.Property(e => e.id_item).HasColumnType("int(11)");

                entity.Property(e => e.lote).HasMaxLength(20);

                entity.Property(e => e.serie).HasMaxLength(20);

                entity.Property(e => e.ubicacion).HasMaxLength(20);

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<detalle_ingreso>(entity =>
            {
                entity.HasIndex(e => e.id_item, "id_item");

                entity.HasIndex(e => e.id_nsalida, "id_nsalida");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cantidad).HasPrecision(12, 2);

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_item).HasColumnType("int(11)");

                entity.Property(e => e.id_nsalida).HasColumnType("int(11)");

                entity.Property(e => e.lote).HasMaxLength(20);

                entity.Property(e => e.serie).HasMaxLength(20);
            });

            modelBuilder.Entity<detalle_salida>(entity =>
            {
                entity.HasIndex(e => e.id_item, "id_item");

                entity.HasIndex(e => e.id_nsalida, "id_nsalida");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cantidad).HasPrecision(12, 2);

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_item).HasColumnType("int(11)");

                entity.Property(e => e.id_nsalida).HasColumnType("int(11)");

                entity.Property(e => e.lote).HasMaxLength(20);

                entity.Property(e => e.serie).HasMaxLength(20);
            });

            modelBuilder.Entity<inventario>(entity =>
            {
                entity.HasIndex(e => e.id_corte, "id_corte");

                entity.HasIndex(e => e.id_item, "id_item");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cantidad)
                    .HasPrecision(12, 2)
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_corte).HasColumnType("int(11)");

                entity.Property(e => e.id_item).HasColumnType("int(11)");

                entity.Property(e => e.lote).HasMaxLength(20);

                entity.Property(e => e.serie).HasMaxLength(20);

                entity.Property(e => e.ubicacion).HasMaxLength(20);

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<item>(entity =>
            {
                entity.HasIndex(e => e.cod_sbcategoria, "cod_sbcategoria");

                entity.HasIndex(e => e.codigo, "codigo")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.codigo).HasMaxLength(15);

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.descripcion).HasMaxLength(100);

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.maximo).HasColumnType("int(11)");

                entity.Property(e => e.minimo).HasColumnType("int(11)");

                entity.Property(e => e.precio).HasPrecision(10, 2);

                entity.Property(e => e.unidad_med).HasMaxLength(6);

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.vida_util).HasColumnType("int(11)");
            });

            modelBuilder.Entity<modulo>(entity =>
            {
                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<nota_ingreso>(entity =>
            {
                entity.HasIndex(e => e.id_suc_origen, "id_suc_origen");

                entity.HasIndex(e => e.id_sucursal, "id_sucursal");

                entity.HasIndex(e => e.nro_doc, "nro_doc")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_suc_origen).HasColumnType("int(11)");

                entity.Property(e => e.id_sucursal).HasColumnType("int(11)");

                entity.Property(e => e.movimiento).HasMaxLength(10);

                entity.Property(e => e.nro_doc).HasColumnType("int(11)");

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.usuario).HasMaxLength(20);
            });

            modelBuilder.Entity<nota_salida>(entity =>
            {
                entity.HasIndex(e => e.id_sucursal, "id_sucursal");

                entity.HasIndex(e => e.nro_doc, "nro_doc")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cod_cliente).HasMaxLength(15);

                entity.Property(e => e.cod_vendedor).HasMaxLength(15);

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_sucursal).HasColumnType("int(11)");

                entity.Property(e => e.nro_doc).HasColumnType("int(11)");

                entity.Property(e => e.prioridad).HasColumnType("smallint(6)");

                entity.Property(e => e.programacion).HasColumnType("smallint(6)");

                entity.Property(e => e.raz_social).HasMaxLength(150);

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<operaciones>(entity =>
            {
                entity.HasIndex(e => e.id_modulo, "id_modulo");

                entity.HasIndex(e => e.id_rol, "id_rol");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.c)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.d)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_modulo).HasColumnType("int(11)");

                entity.Property(e => e.id_rol).HasColumnType("int(11)");

                entity.Property(e => e.r)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.u)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<picking_nsalida>(entity =>
            {
                entity.HasIndex(e => e.id_item, "id_item");

                entity.HasIndex(e => e.id_nsalida, "id_nsalida");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cant_picking).HasPrecision(12, 2);

                entity.Property(e => e.cantidad).HasPrecision(12, 2);

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_item).HasColumnType("int(11)");

                entity.Property(e => e.id_nsalida).HasColumnType("int(11)");

                entity.Property(e => e.lote).HasMaxLength(20);

                entity.Property(e => e.serie).HasMaxLength(20);

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.usuario).HasMaxLength(20);
            });

            modelBuilder.Entity<rack>(entity =>
            {
                entity.HasIndex(e => e.codigo, "codigo")
                    .IsUnique();

                entity.HasIndex(e => e.id_sucursal, "id_sucursal");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cant_colum).HasMaxLength(6);

                entity.Property(e => e.cant_filas).HasMaxLength(6);

                entity.Property(e => e.capacidad_max).HasMaxLength(6);

                entity.Property(e => e.codigo).HasMaxLength(15);

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.descripcion).HasMaxLength(100);

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_sucursal).HasColumnType("int(11)");

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<rol>(entity =>
            {
                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.descripcion).HasMaxLength(255);

                entity.Property(e => e.nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<subcategoria>(entity =>
            {
                entity.HasIndex(e => e.cod_categoria, "cod_categoria");

                entity.HasIndex(e => e.codigo, "codigo")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cod_categoria).HasColumnType("int(11)");

                entity.Property(e => e.codigo).HasMaxLength(15);

                entity.Property(e => e.descripcion).HasMaxLength(80);

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<sucursal>(entity =>
            {
                entity.HasIndex(e => e.codigo, "codigo")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.codigo).HasMaxLength(10);

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.descripcion).HasMaxLength(50);

                entity.Property(e => e.direccion).HasMaxLength(50);

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ubicacion).HasMaxLength(30);

                entity.Property(e => e.updated_at)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<users>(entity =>
            {
                entity.HasIndex(e => e.id_rol, "id_rol");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.apellido).HasMaxLength(40);

                entity.Property(e => e.ci).HasMaxLength(20);

                entity.Property(e => e.created_at)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.direccion).HasMaxLength(80);

                entity.Property(e => e.email).HasMaxLength(20);

                entity.Property(e => e.estado)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.id_rol).HasColumnType("int(11)");

                entity.Property(e => e.nombre).HasMaxLength(40);

                entity.Property(e => e.password).HasMaxLength(255);

                entity.Property(e => e.telefono).HasMaxLength(20);

                entity.Property(e => e.updated_at)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
