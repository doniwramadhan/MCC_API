using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BookingDbContext _context;

        public AccountRepository(BookingDbContext context)
        {
            _context = context;
        }

        //Method CRUD
        public IEnumerable<Account> GetAll()
        {
            return _context.Set<Account>().ToList();     //Untuk cari semua data
        }

        public Account? GetByGuid(Guid guid)
        {
            return _context.Set<Account>().Find(guid);   //Untuk cari data berdasarkan guid
        }
        public Account? Create(Account account)
        {
            try
            {
                _context.Set<Account>().Add(account);   //Untuk memasukan data
                _context.SaveChanges();                       //Untuk execute
                return account;

            }
            catch
            {
                return null;
            }
        }

        public bool Update(Account account)
        {
            try
            {
                _context.Entry(account).State = EntityState.Modified;    //Untuk update data
                _context.SaveChanges();                                     //Untuk execute 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Account account)
        {
            try
            {
                _context.Set<Account>().Remove(account);              //Untuk remove atau delete data
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
