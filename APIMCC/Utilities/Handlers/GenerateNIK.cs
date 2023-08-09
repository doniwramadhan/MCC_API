using APIMCC.Models;
using APIMCC.Repositories;

namespace APIMCC.Utilities.Handlers
{
    public class GenerateNIK
    {
        public static string Nik(string? nik = null)
        {
            if (nik is null)
            {
                return "11111";
            }
                var generateNIK = int.Parse(nik)+1;
                return generateNIK.ToString("D6");
        }
    }
}
