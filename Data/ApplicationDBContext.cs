using Microsoft.EntityFrameworkCore;
using dotnet_Warehouse_Management_System.Products.Entities;
using dotnet_Warehouse_Management_System.Warehouses.Entities;

namespace dotnet_Warehouse_Management_System.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slot> Slots { get; set; }
    }
}
