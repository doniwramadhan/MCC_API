using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {

        }

        public DbSet <Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(e => new
            {
                e.NIK,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            // N : 1
            modelBuilder.Entity<Education>()
                .HasOne(e => e.University)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.UniversityGuid);

            // N : 1
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employee)
                .WithMany(e => e.Bookings)
                .HasForeignKey(e => e.EmployeeGuid);

            // 1 : N
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(r => r.RoomGuid);

            //N : 1
            modelBuilder.Entity<Account>()
                 .HasMany(a => a.AccountRoles)
                 .WithOne(ar => ar.Account)
                 .HasForeignKey(ar => ar.AccountGuid);
            
            //N : 1
            modelBuilder.Entity<Role>()
                .HasMany(r => r.AccountRoles)
                .WithOne(ar => ar.Role)
                .HasForeignKey(ar => ar.RoleGuid);

            //1 : 1
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Employee)
                .WithOne(e => e.Account)
                .HasForeignKey<Account>(a => a.Guid);
            
            //1 : 1
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Education)
                .WithOne(ed => ed.Employee)
                .HasForeignKey<Education>(ed => ed.Guid);


        }
    }
}
