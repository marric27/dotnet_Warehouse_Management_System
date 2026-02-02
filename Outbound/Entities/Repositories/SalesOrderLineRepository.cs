using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Data;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public class SalesOrderLineRepository(ApplicationDBContext context) : BaseRepository<SalesOrderLine>(context), ISalesOrderLineRepository
    {
    }
}
