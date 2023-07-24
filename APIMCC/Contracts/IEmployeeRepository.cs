using APIMCC.Models;

namespace APIMCC.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        string? GetLastNik();
        bool IsNotExist(string value);
    }
}
