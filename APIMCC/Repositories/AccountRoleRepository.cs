using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingDbContext _context;

        public AccountRoleRepository(BookingDbContext context)
        {
            _context = context;
        }

        //Method CRUD
        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList();     //Untuk cari semua data
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRole>().Find(guid);   //Untuk cari data berdasarkan guid
        }
        public AccountRole? Create(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Add(accountRole);   //Untuk memasukan data
                _context.SaveChanges();                       //Untuk execute
                return accountRole;

            }
            catch
            {
                return null;
            }
        }

        public bool Update(AccountRole accountRole)
        {
            try
            {
                _context.Entry(accountRole).State = EntityState.Modified;    //Untuk update data
                _context.SaveChanges();                                     //Untuk execute 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Remove(accountRole);              //Untuk remove atau delete data
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
