using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly BookingDbContext _context;

        public UniversityRepository(BookingDbContext context)
        {
            _context = context;
        }

        //Method CRUD
        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>().ToList();     //Untuk cari semua data
        }

        public University? GetByGuid(Guid guid)
        {
            return _context.Set<University>().Find(guid);   //Untuk cari data berdasarkan guid
        }
        public University? Create(University university)
        {
            try
            {
                _context.Set<University>().Add(university);   //Untuk memasukan data
                _context.SaveChanges();                       //Untuk execute
                return university;

            }
            catch
            {
                return null;
            }
        }

        public bool Update(University university)
        {
            try
            {
                _context.Entry(university).State = EntityState.Modified;    //Untuk update data
                _context.SaveChanges();                                     //Untuk execute 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(University university)
        {
            try
            {
                _context.Set<University>().Remove(university);              //Untuk remove atau delete data
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
