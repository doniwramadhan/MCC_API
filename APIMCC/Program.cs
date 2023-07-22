using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Repositories;
using APIMCC.Services;
using Microsoft.EntityFrameworkCore;

namespace APIMCC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            //Add DbContext to container.
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BookingDbContext>(option => option.UseSqlServer(connection));

            //Add Repositories to container
            builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
            builder.Services.AddScoped<IEducationRepository, EducationRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();

            // Add services to the container.
            builder.Services.AddScoped<UniversityService>();
            builder.Services.AddScoped<RoomService>();
            builder.Services.AddScoped<RoleService>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<EducationService>();
            builder.Services.AddScoped<BookingService>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<AccountRoleService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}