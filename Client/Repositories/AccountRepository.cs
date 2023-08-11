using Client.Contracts;
using System.Net.Http;
using APIMCC.Models;
using APIMCC.DTOs.Accounts;
using APIMCC.Utilities.Handlers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Repositories
{
    public class AccountRepository : GeneralRepository<LoginDto, Guid>, IAccountRepository
    {
        private readonly string request;
        private readonly HttpClient httpClient;
        public AccountRepository(string request = "accounts/") : base(request)
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7231/api/")
            };
        }



        public async Task<ResponseHandler<TokenDto>> Login(LoginDto entity)
        {
            ResponseHandler<TokenDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<TokenDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}