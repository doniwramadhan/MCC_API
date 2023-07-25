using APIMCC.Models;

namespace APIMCC.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        string? GetLastNik();
        string? GetNikByGuid(Guid guid);
        bool IsNotExist(string value);
    }
}
