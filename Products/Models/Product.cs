using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.Products.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Category Category { get; set; }

        public void GenerateCode()
        {
            Code = $"PRO-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}
