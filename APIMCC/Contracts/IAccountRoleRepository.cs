﻿using APIMCC.Models;

namespace APIMCC.Contracts
{
    public interface IAccountRoleRepository
    {
        IEnumerable<AccountRole> GetAll();
        AccountRole? GetByGuid(Guid guid);
        AccountRole? Create(AccountRole accountRole);
        bool Update(AccountRole accountRole);
        bool Delete(AccountRole accountRole);
    }
}
