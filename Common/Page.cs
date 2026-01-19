namespace dotnet_Warehouse_Management_System.Common
{
    public class Page<T>
    {
        public IEnumerable<T> Content { get; set; } = Enumerable.Empty<T>();
        public int TotalElements { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
