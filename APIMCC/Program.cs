using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Repositories;
using APIMCC.Services;
using APIMCC.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

namespace APIMCC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = _context =>
                    {
                        var errors = _context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage);

                        return new BadRequestObjectResult(new ResponseHandlerValidator
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "Validation Error",
                            Errors = errors.ToArray()
                        });
                    };
                });
        
            //Add SmtpClient to container.
            builder.Services.AddTransient<IEmailHandler,EmailHandler>(_ => new EmailHandler(builder.Configuration["EmailService:SmtpServer"],
                int.Parse(builder.Configuration["EmailService:SmtpPort"]), builder.Configuration["EmailService:FromEmailAddress"]
                ));
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

            // Add FluentValidation
            builder.Services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


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