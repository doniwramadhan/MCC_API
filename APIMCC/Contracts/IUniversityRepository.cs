using APIMCC.Models;

namespace APIMCC.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University>
    {
       University? GetByCode (string code);
    }
}
