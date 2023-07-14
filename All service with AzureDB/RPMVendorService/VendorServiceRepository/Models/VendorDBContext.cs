using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VendorServiceRepository
{
    public partial class VendorDBContext : DbContext
    {
        public VendorDBContext()
        {
        }

        public VendorDBContext(DbContextOptions<VendorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Vendor_Stock> Vendor_Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:rpmserver.database.windows.net,1433;Initial Catalog=VendorDB;Persist Security Info=False;User ID=rpmadmin;Password=qwerty@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Vendor_Id)
                    .HasName("PK__Vendor__D9CCC2A87982C08E");

                entity.ToTable("Vendor");

                entity.Property(e => e.Vendor_Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vendor_Stock>(entity =>
            {
                entity.HasKey(e => new { e.Product_Id, e.Vendor_Id })
                    .HasName("PK_VendorStock_PIDVID");

                entity.ToTable("Vendor_Stock");

                entity.Property(e => e.Expected_StockReplenish_Date).HasColumnType("datetime");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Vendor_Stocks)
                    .HasForeignKey(d => d.Vendor_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vendor_St__Vendo__25869641");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
