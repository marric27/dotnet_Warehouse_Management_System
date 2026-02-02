using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class StockUnitResponseDto
    {
        public long Id { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ProductCode { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public ProductResponseDto ProductDto { get; set; }
        public long? SlotId { get; set; }
    }
}