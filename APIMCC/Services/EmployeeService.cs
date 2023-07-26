using APIMCC.Contracts;
using APIMCC.DTOs.Employees;
using APIMCC.DTOs.Rooms;
using APIMCC.Models;
using APIMCC.Utilities.Handlers;

namespace APIMCC.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
        {
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
        }

        public IEnumerable<MasterEmployeeDto> GetAllEmployeeDetail()
        {
            var employees = _employeeRepository.GetAll();

            if (!employees.Any())
            {
                return Enumerable.Empty<MasterEmployeeDto>();
            }

            var employeesDetailDto = new List<MasterEmployeeDto>();

            foreach (var emp in employees)
            {
                var education = _educationRepository.GetByGuid(emp.Guid);
                var university = _universityRepository.GetByGuid(education.UniversityGuid);

                MasterEmployeeDto employeeDetail = new MasterEmployeeDto
                {
                    EmployeeGuid = emp.Guid,
                    NIK = emp.NIK,
                    FullName = emp.FirstName + " " + emp.LastName,
                    BirthDate = emp.BirthDate,
                    Gender = emp.Gender,
                    HireDate = emp.HireDate,
                    Email = emp.Email,
                    PhoneNumber = emp.PhoneNumber,
                    Major = education.Major,
                    Degree = education.Degree,
                    GPA = education.GPA,
                    UniversityName = university.Name
                };

                employeesDetailDto.Add(employeeDetail);
            };

            return employeesDetailDto; // employeeDetail is found;
        }

        public MasterEmployeeDto? GetAllEmployeeByGuid(Guid guid)
        {
            var emp = _employeeRepository.GetByGuid(guid);

            if(emp is null)
            {
                return null;
            }
            var employeesDetailDto = new List<MasterEmployeeDto>();

            
                var education = _educationRepository.GetByGuid(emp.Guid);
                var university = _universityRepository.GetByGuid(education.UniversityGuid);

                   return new MasterEmployeeDto
                {
                    EmployeeGuid = emp.Guid,
                    NIK = emp.NIK,
                    FullName = emp.FirstName + " " + emp.LastName,
                    BirthDate = emp.BirthDate,
                    Gender = emp.Gender,
                    HireDate = emp.HireDate,
                    Email = emp.Email,
                    PhoneNumber = emp.PhoneNumber,
                    Major = education.Major,
                    Degree = education.Degree,
                    GPA = education.GPA,
                    UniversityName = university.Name
                };         
        }
        public IEnumerable<EmployeeDto> GetAll()
        {
            var emp = _employeeRepository.GetAll();
            if (!emp.Any())
            {
                return Enumerable.Empty<EmployeeDto>();
            }

            var empDto = new List<EmployeeDto>();
            foreach (var emps in emp)
            {
                empDto.Add((EmployeeDto)emps);
            }
            return empDto;
        }

        public EmployeeDto? GetByGuid(Guid guid)
        {
            var emp = _employeeRepository.GetByGuid(guid);
            if (emp == null)
            {
                return null;
            }
            return (EmployeeDto)emp;
        }

        public EmployeeDto? Create(NewEmployeeDto newEmployeeDto)
        {
            Employee toCreate = newEmployeeDto;
            toCreate.NIK = GenerateNIK.Nik(_employeeRepository.GetLastNik());

            var emp = _employeeRepository.Create(toCreate);
            if (toCreate == null)
            {
                return null;
            }
            return (EmployeeDto) toCreate;
        }

        public int Update(UpdateEmployeeDto updateEmployeeDto)
        {

            
            var emp = _employeeRepository.GetByGuid(updateEmployeeDto.Guid);
            if (emp is null)
            {
                return -1;
            }

            Employee getNikByGuid = updateEmployeeDto;

            getNikByGuid.NIK = _employeeRepository.GetNikByGuid(updateEmployeeDto.Guid);
            if (getNikByGuid is null)
            {
                return -1;
            }
            _employeeRepository.Update(getNikByGuid);

            getNikByGuid.CreatedDate = emp.CreatedDate;
            var result = _employeeRepository.Update(getNikByGuid);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var emp = _employeeRepository.GetByGuid(guid);
            if (emp is null)
            {
                return -1;
            }

            var result = _employeeRepository.Delete(emp);

            return result ? 1 : 0;
        }
    }
}
