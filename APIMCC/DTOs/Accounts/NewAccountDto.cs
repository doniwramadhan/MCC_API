using APIMCC.Models;

namespace APIMCC.DTOs.Accounts
{
    public class NewAccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredDate { get; set; }

        public static implicit operator Account(NewAccountDto newAccountDto)
        {
            return new Account
            {
                Guid = newAccountDto.Guid,
                Password = newAccountDto.Password,
                OTP = newAccountDto.OTP,
                IsUsed = newAccountDto.IsUsed,
                ExpiredDate = newAccountDto.ExpiredDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewAccountDto(Account account)
        {
            return new NewAccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
                OTP = account.OTP,
                IsUsed = account.IsUsed,
                ExpiredDate = account.ExpiredDate,
            };
        }
    }
}
