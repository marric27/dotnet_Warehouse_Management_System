namespace dotnet_Warehouse_Management_System.Common
{
    public static class PageMapper
    {
        public static Page<TDestination> Map<TSource, TDestination>(
            this Page<TSource> source,
            Func<TSource, TDestination> mapFunc)
        {
            return new Page<TDestination>
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                TotalElements = source.TotalElements,
                Content = source.Content
                    .Select(mapFunc)
                    .ToList()
            };
        }
    }
}
