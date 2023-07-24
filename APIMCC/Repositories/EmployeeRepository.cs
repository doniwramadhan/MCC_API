﻿using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using APIMCC.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIMCC.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context) { }

        public bool IsNotExist(string value)
        {
            return _context.Set<Employee>()
                .SingleOrDefault(e => e.Email.Contains(value)
                || e.PhoneNumber.Contains(value)) is null;
        }

        public string GetLastNik()
        {
            return _context.Set<Employee>().ToList().LastOrDefault()?.NIK;
        }

        
    }
}
