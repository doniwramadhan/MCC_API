using APIMCC.Contracts;
using APIMCC.DTOs.Accounts;
using APIMCC.DTOs.Bookings;
using APIMCC.DTOs.Employees;
using APIMCC.Models;
using APIMCC.Utilities.Handlers;

namespace APIMCC.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IEducationRepository educationRepository, 
            IUniversityRepository universityRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
        }

        public int ForgotPasswordDto(ForgotPasswordDto forgotPasswordDto)
        {
            var emp = _employeeRepository.GetByEmail(forgotPasswordDto.Email);
            if (emp is null)
            {
                return 0;
            }
            var acc = _accountRepository.GetByGuid(emp.Guid);
            if(acc is null)
            {
                return -1;
            }
            var otp = new Random().Next(111111, 999999);
            var isUpdated = _accountRepository.Update(new Account
            {
                Guid = acc.Guid,
                Password = acc.Password,
                ExpiredDate = DateTime.Now.AddMinutes(5),
                OTP = otp,
                IsUsed = false,
                CreatedDate = acc.CreatedDate,
                ModifiedDate = DateTime.Now
            });

            if(!isUpdated)
            {
                return -1;
            }

            forgotPasswordDto.Email = $"{otp}";
            return 1;
        }
        
        //
        //  Change Password
        //
        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var isExist = _employeeRepository.GetByEmail(changePasswordDto.Email);
            if(isExist is null)
            {
                return -1;
            }
            var getAcc = _accountRepository.GetByGuid(isExist.Guid);
            var acc = new Account
            {
                Guid = getAcc.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAcc.CreatedDate,
                OTP = getAcc.OTP,
                ExpiredDate = getAcc.ExpiredDate,
                Password = changePasswordDto.NewPassword
            };
            if(getAcc.OTP != changePasswordDto.OTP)
            {
                return 0;
            }
            if(getAcc.IsUsed == true)
            {
                return 1;
            }
            if(getAcc.ExpiredDate < DateTime.Now)
            {
                return 2;
            }
            var isUpdated = _accountRepository.Update(acc);
            if (!isUpdated)
            {
                return 0;
            }
            return 3;
        }
        public RegisterDto? Register(RegisterDto registerDto)
        {
            Employee toCreate = registerDto;
            toCreate.NIK = GenerateNIK.Nik(_employeeRepository.GetLastNik());

            var createAccount = _employeeRepository.Create(toCreate);
            if (createAccount is null)
            {
                return null;
            }
            _educationRepository.Create(registerDto);
            _universityRepository.Create(registerDto);
            _accountRepository.Create(registerDto);
            

            return registerDto;
        }
        public int Login(LoginDto loginDto)
        {
            var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);
            if(getEmployee == null)
            {
                return 0;
            }

            var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);
            if(getAccount.Password == loginDto.Password)
            {
                return 1;
            }

            return 0;
        }
        public IEnumerable<AccountDto> GetAll()
        {
            var acc = _accountRepository.GetAll();
            if (!acc.Any())
            {
                return Enumerable.Empty<AccountDto>();
            }

            var accDto = new List<AccountDto>();
            foreach (var accs in acc)
            {
                accDto.Add((AccountDto)accs);
            }
            return accDto;
        }

        public AccountDto? GetByGuid(Guid guid)
        {
            var acc = _accountRepository.GetByGuid(guid);
            if (acc is null)
            {
                return null;
            }
            return (AccountDto)acc;
        }

        public AccountDto? Create(NewAccountDto newAccountDto)
        {
            var acc = _accountRepository.Create(newAccountDto);
            if (acc == null)
            {
                return null;
            }
            return (AccountDto)acc;
        }

        public int Update(AccountDto accountDto)
        {
            var acc = _accountRepository.GetByGuid(accountDto.Guid);
            if (acc is null)
            {
                return -1;
            }
            Account toUpdate = accountDto;
            toUpdate.CreatedDate = acc.CreatedDate;
            var result = _accountRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var acc = _accountRepository.GetByGuid(guid);
            if (acc is null)
            {
                return -1;
            }

            var result = _accountRepository.Delete(acc);

            return result ? 1 : 0;
        }
    }
}
