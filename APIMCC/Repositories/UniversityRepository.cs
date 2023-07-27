using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingDbContext context) : base (context) { }

        public University? GetByCode(string code)
        {
            return _context.Set<University>().SingleOrDefault(u => u.Code == code);
        }

       
    }
}
