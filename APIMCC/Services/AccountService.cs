using APIMCC.Contracts;
using APIMCC.Data;
using APIMCC.DTOs.Accounts;
using APIMCC.DTOs.Bookings;
using APIMCC.DTOs.Employees;
using APIMCC.Models;
using APIMCC.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace APIMCC.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly BookingDbContext _dbContext;
        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IEducationRepository educationRepository, 
            IUniversityRepository universityRepository, BookingDbContext bookingDbContext)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _dbContext = bookingDbContext;
        }

        //
        //  Forgot Password
        //
        public int ForgotPasswordDto(ForgotPasswordDto forgotPasswordDto)
        {
            var getAccountDetail = (from e in _employeeRepository.GetAll() 
                                    join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                    where e.Email == forgotPasswordDto.Email
                                    select a).FirstOrDefault();


            var otp = new Random().Next(111111, 999999);
            var account = new Account
            {
                Guid = getAccountDetail.Guid,
                Password = getAccountDetail.Password,
                ExpiredDate = DateTime.Now.AddMinutes(5),
                OTP = otp,
                IsUsed = false,
                CreatedDate = getAccountDetail.CreatedDate,
                ModifiedDate = DateTime.Now
            };

            var isUpdated = _accountRepository.Update(account);
    
            if(!isUpdated)
            {
                return -1;
            }

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
        //
        //  Register Account
        //
        public int Register(RegisterDto registerDto)
        {
            if (!_employeeRepository.IsNotExist(registerDto.Email) ||
                !_employeeRepository.IsNotExist(registerDto.PhoneNumber))
            {
                return 0; // kalau sudah ada, pendaftaran gagal.
            }

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var university = _universityRepository.GetByCode(registerDto.Code);
                if (university is null)
                {
                    // Jika universitas belum ada, buat objek University baru dan simpan
                    var createUniversity = _universityRepository.Create(new University
                    {
                        Code = registerDto.Code,
                        Name = registerDto.Name
                    });

                    university = createUniversity;
                }

                var newNik = GenerateNIK.Nik(_employeeRepository
                                           .GetLastNik()); //karena niknya generate, sebelumnya kalo ga dikasih ini niknya null jadi error
                var employeeGuid = Guid.NewGuid(); // Generate GUID baru untuk employee

                // Buat objek Employee dengan nilai GUID baru
                var employee = _employeeRepository.Create(new Employee
                {
                    Guid = employeeGuid, //ambil dari variabel yang udah dibuat diatas
                    NIK = newNik,        //ini juga
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    BirthDate = registerDto.BirthDate,
                    Gender = registerDto.Gender,
                    HireDate = registerDto.HireDate,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber
                });


                var education = _educationRepository.Create(new Education
                {
                    Guid = employeeGuid, // Gunakan employeeGuid
                    Major = registerDto.Major,
                    Degree = registerDto.Degree,
                    GPA = registerDto.GPA,
                    UniversityGuid = university.Guid
                });

                var account = _accountRepository.Create(new Account
                {
                    Guid = employeeGuid, // Gunakan employeeGuid
                    OTP = 1,             //sementara ini dicoba gabisa diisi angka nol didepan, tadi masukin 098 error
                    IsUsed = true,
                    Password = registerDto.Password
                });
                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return -1;
            }
        }
        //
        //  Login Account
        //
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

        //
        //  Get All Account
        //
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

        //
        //  Get by Guid
        //
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

        //
        //  Update Account
        //
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

        //
        // Delete Account
        //
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
