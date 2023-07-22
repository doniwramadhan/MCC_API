using APIMCC.DTOs.Bookings;
using APIMCC.Models;

namespace APIMCC.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid Guid { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredDate { get; set; }

        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                Guid = accountDto.Guid,
                OTP = accountDto.OTP,
                IsUsed = accountDto.IsUsed,
                ExpiredDate = accountDto.ExpiredDate,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Guid = account.Guid,
                OTP = account.OTP,
                IsUsed = account.IsUsed,
                ExpiredDate = account.ExpiredDate,
            };
        }
    }
}
