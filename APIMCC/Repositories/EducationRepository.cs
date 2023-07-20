using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly BookingDbContext _context;

        public EducationRepository(BookingDbContext context)
        {
            _context = context;
        }

        //Method CRUD
        public IEnumerable<Education> GetAll()
        {
            return _context.Set<Education>().ToList();     //Untuk cari semua data
        }

        public Education? GetByGuid(Guid guid)
        {
            return _context.Set<Education>().Find(guid);   //Untuk cari data berdasarkan guid
        }
        public Education? Create(Education education)
        {
            try
            {
                _context.Set<Education>().Add(education);   //Untuk memasukan data
                _context.SaveChanges();                       //Untuk execute
                return education;

            }
            catch
            {
                return null;
            }
        }

        public bool Update(Education education)
        {
            try
            {
                _context.Entry(education).State = EntityState.Modified;    //Untuk update data
                _context.SaveChanges();                                     //Untuk execute 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Education education)
        {
            try
            {
                _context.Set<Education>().Remove(education);              //Untuk remove atau delete data
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
