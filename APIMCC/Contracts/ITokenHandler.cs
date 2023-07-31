using System.Security.Claims;

namespace APIMCC.Contracts
{
    public interface ITokenHandler
    {
        string? GenerateToken(IEnumerable<Claim> claims);
    }
}
