using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BookingDbContext _context;

        public RoleRepository(BookingDbContext context)
        {
            _context = context;
        }

        //Method CRUD
        public IEnumerable<Role> GetAll()
        {
            return _context.Set<Role>().ToList();     //Untuk cari semua data
        }

        public Role? GetByGuid(Guid guid)
        {
            return _context.Set<Role>().Find(guid);   //Untuk cari data berdasarkan guid
        }
        public Role? Create(Role role)
        {
            try
            {
                _context.Set<Role>().Add(role);   //Untuk memasukan data
                _context.SaveChanges();                       //Untuk execute
                return role;

            }
            catch
            {
                return null;
            }
        }

        public bool Update(Role role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;    //Untuk update data
                _context.SaveChanges();                                     //Untuk execute 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Role role)
        {
            try
            {
                _context.Set<Role>().Remove(role);              //Untuk remove atau delete data
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
