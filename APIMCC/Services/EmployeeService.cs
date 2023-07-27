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
            var employeesDetail = from employee in _employeeRepository.GetAll()
                                  join education in _educationRepository.GetAll() on employee.Guid equals education.Guid
                                  join university in _universityRepository.GetAll() on education.UniversityGuid equals university.Guid
                                  select new MasterEmployeeDto
                                  {
                                      EmployeeGuid = employee.Guid,
                                      NIK = employee.NIK,
                                      FullName = employee.FirstName + " " + employee.LastName,
                                      BirthDate = employee.BirthDate,
                                      Gender = employee.Gender,
                                      HireDate = employee.HireDate,
                                      Email = employee.Email,
                                      PhoneNumber = employee.PhoneNumber,
                                      Major = education.Major,
                                      Degree = education.Degree,
                                      GPA = education.GPA,
                                      UniversityName = university.Name
                                  };

            return employeesDetail;

            
        }

        public MasterEmployeeDto? GetAllEmployeeByGuid(Guid guid)
        {
            return GetAllEmployeeDetail().SingleOrDefault(e => e.EmployeeGuid == guid);
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
