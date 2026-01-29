using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_Warehouse_Management_System.Products.Entities;
using dotnet_Warehouse_Management_System.Warehouses.Entities;
using dotnet_Warehouse_Management_System.Common;


namespace dotnet_Warehouse_Management_System.GoodsIn.Entities
{
    public class StockUnit
    {
        public long Id { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ProductCode { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public long? SlotId { get; set; }
        //public Slot? Slot { get; set; }


        public void GenerateCode()
        {
            Code = $"STK-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}