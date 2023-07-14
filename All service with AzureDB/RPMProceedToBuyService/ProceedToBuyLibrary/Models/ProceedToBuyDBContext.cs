using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProceedToBuyLibrary
{
    public partial class ProceedToBuyDBContext : DbContext
    {
        public ProceedToBuyDBContext()
        {
        }

        public ProceedToBuyDBContext(DbContextOptions<ProceedToBuyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<MiniUser> MiniUsers { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:rpmserver.database.windows.net,1433;Initial Catalog=ProceedToBuyDB;Persist Security Info=False;User ID=rpmadmin;Password=qwerty@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId })
                    .HasName("PK_UIDVIDPID");

                entity.ToTable("Cart");

                entity.Property(e => e.ExpectedDelivery).HasColumnType("datetime");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__UserId__4222D4EF");
            });

            modelBuilder.Entity<MiniUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__MiniUser__1788CC4CE36736BB");

                entity.ToTable("MiniUser");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.ZipCode).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId })
                    .HasName("PK_Wishlist_UIDVIDPID");

                entity.ToTable("Wishlist");

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
