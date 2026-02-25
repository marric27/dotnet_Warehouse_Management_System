using Microsoft.EntityFrameworkCore;
using dotnet_Warehouse_Management_System.Products.Entities;
using dotnet_Warehouse_Management_System.Warehouses.Entities;
using dotnet_Warehouse_Management_System.GoodsIn.Entities;
using dotnet_Warehouse_Management_System.Customers.Entities;
using dotnet_Warehouse_Management_System.Outbound.Entities;
using dotnet_Warehouse_Management_System.Picking.Entities;

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

            modelBuilder.Entity<Picklist>()
                .HasIndex(o => o.Code).IsUnique();
            modelBuilder.Entity<Picklist>()
                .HasMany(p => p.PicklistItemList)
                .WithOne(i => i.Picklist)
                .HasForeignKey(i => i.PicklistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Slot>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Slots)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PicklistItem>()
                .HasMany(p => p.PickingInfos)
                .WithOne(pi => pi.PicklistItem)
                .HasForeignKey(pi => pi.PicklistItemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PicklistItem>()
                .HasIndex(pi => pi.code)
                .IsUnique();
            modelBuilder.Entity<PickingInfo>()
                .HasOne(p => p.PicklistItem)
                .WithMany(p => p.PickingInfos)
                .HasForeignKey(p => p.PicklistItemId);

            modelBuilder.Entity<GrnItem>()
                .HasMany(c => c.CheckingInfoList)
                .WithOne(ci => ci.GrnItem)
                .HasForeignKey(ci => ci.GrnItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Code)
                .IsUnique();

            modelBuilder.Entity<StockUnit>()
                .HasIndex(su => su.Code)
                .IsUnique();

            modelBuilder.Entity<CheckingInfo>()
                .HasIndex(ci => ci.Code)
                .IsUnique();


            //modelBuilder.Entity<Slot>()
            //    .HasMany(s => s.StockUnits)
            //    .WithOne(su => su.Slot)
            //    .HasForeignKey(s => s.SlotId)
            //    .OnDelete(DeleteBehavior.SetNull);
            //modelBuilder.Entity<StockUnit>()
            //    .HasOne(su => su.Slot)
            //    .WithMany(s => s.StockUnits)
            //    .HasForeignKey(s => s.SlotId)
            //    .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<CheckingInfo>()
                .HasOne(ci => ci.StockUnit)
                .WithMany()
                .HasForeignKey(ci => ci.StockUnitId)
                .IsRequired();



        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Grn> Grns { get; set; }
        public DbSet<GrnItem> GrnItems { get; set; }
        public DbSet<StockUnit> StockUnits { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; private set; }
        public DbSet<SalesOrderLine> SalesOrderLines { get; set; }
        public DbSet<Picklist> Picklists { get; set; }
        public DbSet<PicklistItem> PicklistItems { get; set; }
        public DbSet<PickingInfo> PickingInfos { get; set; }
        public DbSet<CheckingInfo> CheckingInfos { get; set; }
    }
}