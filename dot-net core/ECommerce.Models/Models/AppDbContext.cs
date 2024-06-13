using Microsoft.EntityFrameworkCore;

namespace ECommerce.Models.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // this will disable on delete cascade on all foreign keys, which is by default.
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            foreach (var fkey in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                fkey.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public  DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
