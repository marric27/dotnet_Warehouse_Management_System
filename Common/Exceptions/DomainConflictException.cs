namespace dotnet_Warehouse_Management_System.Common.Exceptions
{
    public class DomainConflictException : DomainException
    {
        public DomainConflictException(string message) : base(message)
        {
        }
    }
}
