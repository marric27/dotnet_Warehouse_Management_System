using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.Slots.Helpers
{
    public class QueryObject
    {
        public Category? Category { get; set; } = null;
        public string? Code { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
