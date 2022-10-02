using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer
{
    public partial class MultiShopContext:DbContext
    {
        public MultiShopContext()
        {
        }
        public MultiShopContext(DbContextOptions<MultiShopContext> options)
            : base(options)
        {
        }

      
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
               

                entity.HasOne(d => d.ProductCaregory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCaregoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.ProductColor)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Colors");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
