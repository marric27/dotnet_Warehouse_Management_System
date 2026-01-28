using dotnet_Warehouse_Management_System.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class StockUnitRequestDto
    {
        public string BatchNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public string? ProductCode { get; set; }
        public Category Category { get; set; }
    }
}