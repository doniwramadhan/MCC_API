using APIMCC.Models;

namespace APIMCC.Contracts
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
    {
        IEnumerable<string>? GetRoleNamesByAccountGuid(Guid guid);
    }
}
