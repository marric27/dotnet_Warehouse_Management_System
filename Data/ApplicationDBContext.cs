using Microsoft.EntityFrameworkCore;
using dotnet_Warehouse_Management_System.Products.Entities;
using dotnet_Warehouse_Management_System.Warehouses.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.Customers.Entities;
using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grn>()
                .HasIndex(g => g.Code)
                .IsUnique();
            modelBuilder.Entity<GrnItem>()
                .HasIndex(g => g.Code)
                .IsUnique();
            modelBuilder.Entity<Grn>()
                .HasMany(g => g.Items)
                .WithOne(i => i.Grn)
                .HasForeignKey(i => i.GrnId)
                .IsRequired();
            modelBuilder.Entity<GrnItem>()
                .HasOne(i => i.Grn)
                .WithMany(g => g.Items)
                .HasForeignKey(i => i.GrnId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.Code)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.SalesOrderLineList)
                .WithOne(l => l.Order)
                .HasForeignKey(l => l.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Grn> Grns { get; set; }
        public DbSet<GrnItem> GrnItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; private set; }
        public DbSet<SalesOrderLine> SalesOrderLines { get; set; }
    }
}
