using APIMCC.Models;

namespace APIMCC.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        bool IsNotExist(string value);
    }
}
