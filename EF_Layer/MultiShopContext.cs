using CoreLayer.consts;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer
{
    public partial class MultiShopContext:IdentityDbContext
    {
        public MultiShopContext()
        {
        }
        public MultiShopContext(DbContextOptions<MultiShopContext> options)
            : base(options)
        {
        }

      
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Discount> Discounts{ get; set; }
        public virtual DbSet<CartItem> CartItems{ get; set; }
        public virtual DbSet<UserData> UsersData{ get; set; }
        public virtual DbSet<Order> Orders{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>( entity =>
            {
               

                entity.HasOne(d => d.ProductCaregory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCaregoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");

                //entity.HasMany(d => d.ProductColor)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.ProductColorId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Products_Colors");

                 
            });

     
           
            modelBuilder.Entity<Color>()
                .HasData(new Color { Id=(int)ColorsIds.black, ColorName = "black" },
                         new Color { Id = (int)ColorsIds.white, ColorName = "white" },
                         new Color { Id = (int)ColorsIds.red, ColorName = "red" },
                         new Color { Id = (int)ColorsIds.green, ColorName = "green" },
                         new Color { Id = (int)ColorsIds.blue, ColorName = "blue" },
                         new Color { Id = (int)ColorsIds.magenta, ColorName = "magent" },
                         new Color { Id = (int)ColorsIds.cyan, ColorName = "cyan" },
                         new Color { Id = (int)ColorsIds.turquoise, ColorName = "turquo" },
                         new Color { Id = (int)ColorsIds.brown, ColorName = "brown" },
                         new Color { Id = (int)ColorsIds.grey, ColorName = "grey" },
                         new Color { Id = (int)ColorsIds.beige, ColorName = "beige" },
                         new Color { Id = (int)ColorsIds.pink, ColorName = "pink" },
                         new Color { Id = (int)ColorsIds.purple, ColorName = "purple" }
                );

             base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
