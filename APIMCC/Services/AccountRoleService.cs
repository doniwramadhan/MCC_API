using APIMCC.Contracts;
using APIMCC.DTOs.AccountRoles;
using APIMCC.DTOs.Universities;
using APIMCC.Models;

namespace APIMCC.Services
{
    public class AccountRoleService
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        public IEnumerable<AccountRoleDto> GetAll()
        {
            var ar = _accountRoleRepository.GetAll();
            if (!ar.Any())
            {
                return Enumerable.Empty<AccountRoleDto>();
            }

            var arDto = new List<AccountRoleDto>();
            foreach (var ars in ar)
            {
                arDto.Add((AccountRoleDto)ars);
            }
            return arDto;
        }

        public AccountRoleDto? GetByGuid(Guid guid)
        {
            var ar = _accountRoleRepository.GetByGuid(guid);
            if (ar == null)
            {
                return null;
            }
            return (AccountRoleDto)ar;
        }

        public AccountRoleDto? Create(NewAccountRoleDto newAccountRoleDto)
        {
            var ar = _accountRoleRepository.Create(newAccountRoleDto);
            if (ar == null)
            {
                return null;
            }
            return (AccountRoleDto)ar;
        }

        public int Update(AccountRoleDto accountRoleDto)
        {
            var ar = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
            if (ar is null)
            {
                return -1;
            }
            AccountRole toUpdate = accountRoleDto;
            toUpdate.CreatedDate = ar.CreatedDate;
            var result = _accountRoleRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var ar = _accountRoleRepository.GetByGuid(guid);
            if (ar is null)
            {
                return -1;
            }

            var result = _accountRoleRepository.Delete(ar);

            return result ? 1 : 0;
        }
    }
}
