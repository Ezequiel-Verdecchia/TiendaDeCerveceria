using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Cerveceria.Web.DAO.Models
{
    public partial class cerveceriaContext : DbContext
    {
        public cerveceriaContext()
        {
        }

        public cerveceriaContext(DbContextOptions<cerveceriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FacturasCabecera> FacturasCabeceras { get; set; }
        public virtual DbSet<FacturasDetalle> FacturasDetalles { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedore> Proveedores { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data source=DESKTOP-5BNP4ST\\SQLEXPRESS;Initial Catalog=cerveceria;Integrated Security=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<FacturasCabecera>(entity =>
            {
                entity.ToTable("Facturas_Cabecera");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cae).HasColumnName("CAE");

                entity.Property(e => e.CodigoBarra).HasColumnName("Codigo_Barra");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaCae)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Cae");

                entity.Property(e => e.Ifecha)
                    .HasColumnType("datetime")
                    .HasColumnName("IFecha");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UsuarioId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.FacturasCabeceras)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facturas_Cabecera_Usuarios");
            });

            modelBuilder.Entity<FacturasDetalle>(entity =>
            {
                entity.HasKey(e => new { e.FacturaId, e.DetalleId });

                entity.ToTable("Facturas_Detalle");

                entity.Property(e => e.DetalleId).ValueGeneratedOnAdd();

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Precio_Unitario");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Costo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Descripcion).IsRequired();

                entity.Property(e => e.Ifecha)
                    .HasColumnType("datetime")
                    .HasColumnName("IFecha");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Medida)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productos_Proveedores");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.Property(e => e.Cuit)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Razon Social");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
