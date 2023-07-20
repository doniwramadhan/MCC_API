using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookingDbContext _context;

        public EmployeeRepository(BookingDbContext context)
        {
            _context = context;
        }

        //Method CRUD
        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();     //Untuk cari semua data
        }

        public Employee? GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Find(guid);   //Untuk cari data berdasarkan guid
        }
        public Employee? Create(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Add(employee);   //Untuk memasukan data
                _context.SaveChanges();                       //Untuk execute
                return employee;

            }
            catch
            {
                return null;
            }
        }

        public bool Update(Employee employee)
        {
            try
            {
                _context.Entry(employee).State = EntityState.Modified;    //Untuk update data
                _context.SaveChanges();                                     //Untuk execute 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Remove(employee);              //Untuk remove atau delete data
                _context.SaveChanges();                                     //Untuk execute
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
