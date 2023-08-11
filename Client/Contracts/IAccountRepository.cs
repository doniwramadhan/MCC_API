using APIMCC.DTOs.Accounts;
using APIMCC.DTOs.Employees;
using APIMCC.Models;
using APIMCC.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<LoginDto, Guid>
    {
        Task<ResponseHandler<TokenDto>> Login(LoginDto login);
    }
}
