using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingDbContext _context;

        public RoomRepository(BookingDbContext context)
        {
            _context = context;
        }

        //Method CRUD
        public IEnumerable<Room> GetAll()
        {
            return _context.Set<Room>().ToList();     //Untuk cari semua data
        }

        public Room? GetByGuid(Guid guid)
        {
            return _context.Set<Room>().Find(guid);   //Untuk cari data berdasarkan guid
        }
        public Room? Create(Room room)
        {
            try
            {
                _context.Set<Room>().Add(room);   //Untuk memasukan data
                _context.SaveChanges();                       //Untuk execute
                return room;

            }
            catch
            {
                return null;
            }
        }

        public bool Update(Room room)
        {
            try
            {
                _context.Entry(room).State = EntityState.Modified;    //Untuk update data
                _context.SaveChanges();                                     //Untuk execute 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Room room)
        {
            try
            {
                _context.Set<Room>().Remove(room);              //Untuk remove atau delete data
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
