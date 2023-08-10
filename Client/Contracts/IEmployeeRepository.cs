using APIMCC.DTOs.Employees;
using APIMCC.Models;

namespace Client.Contracts
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
    }
}
