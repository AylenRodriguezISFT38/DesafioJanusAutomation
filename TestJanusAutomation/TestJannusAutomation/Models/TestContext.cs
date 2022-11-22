using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestJannusAutomation.Models
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<TipoProducto> TipoProductos { get; set; }
        public virtual DbSet<VwStockProducto> VwStockProductos { get; set; }
        public virtual DbSet<VwStockProductoTipo> VwStockProductoTipos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-MCDM1MI;Database=Test;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.HasIndex(e => e.IdTipoProducto, "IX_Producto_IdTipoProducto");

                entity.Property(e => e.CreatedAt).HasColumnName("Created_At");

                entity.HasOne(d => d.IdTipoProductoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdTipoProducto);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.HasIndex(e => e.IdProducto, "IX_Stock_IdProducto");

                entity.Property(e => e.CreatedAt).HasColumnName("Created_At");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.IdProducto);
            });

            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.ToTable("TipoProducto");

                entity.Property(e => e.CreatedAt).HasColumnName("Created_At");
            });

            modelBuilder.Entity<VwStockProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_StockProducto");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<VwStockProductoTipo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_StockProductoTipo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
