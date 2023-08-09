using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using APIMCC.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIMCC.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context) { }

        public bool IsNotExist(string value)
        {
            return _context.Set<Employee>()
                .SingleOrDefault(e => e.Email.Contains(value)
                || e.PhoneNumber.Contains(value)) is null;
        }



        
        public string GetLastNik()
        {
            var employees = _context.Set<Employee>().ToList().LastOrDefault().NIK;
            if (employees == null)
            {
                return "last NIK not found";
            };
            return employees;
        }

        public string GetNikByGuid(Guid guid)
        {
            Employee emp = _context.Set<Employee>().FirstOrDefault(e => e.Guid == guid);
            return emp?.NIK ;
        }

        public Employee? GetByEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
        }
    }
}
