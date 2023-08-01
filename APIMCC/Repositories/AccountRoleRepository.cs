using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMCC.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingDbContext context) : base(context) { }

        public IEnumerable<string>? GetRoleNamesByAccountGuid(Guid guid)
        {
            var result = _context.Set<AccountRole>()
                                 .Where(ar => ar.AccountGuid == guid)
                                 .Include(ar => ar.Role)
                                 .Select(ar => ar.Role!.Name);

            return result;
        }
    }
}
