using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        //Method CRUD
        public IEnumerable<Booking> GetAll()
        {
            return _context.Set<Booking>().ToList();     //Untuk cari semua data
        }

        public Booking? GetByGuid(Guid guid)
        {
            return _context.Set<Booking>().Find(guid);   //Untuk cari data berdasarkan guid
        }
        public Booking? Create(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Add(booking);   //Untuk memasukan data
                _context.SaveChanges();                       //Untuk execute
                return booking;

            }
            catch
            {
                return null;
            }
        }

        public bool Update(Booking booking)
        {
            try
            {
                _context.Entry(booking).State = EntityState.Modified;    //Untuk update data
                _context.SaveChanges();                                     //Untuk execute 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Remove(booking);              //Untuk remove atau delete data
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
