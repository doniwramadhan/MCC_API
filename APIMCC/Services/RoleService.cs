using APIMCC.Contracts;
using APIMCC.DTOs.Roles;
using APIMCC.DTOs.Rooms;
using APIMCC.Models;
using APIMCC.Repositories;

namespace APIMCC.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<RoleDto> GetAll()
        {
            var role = _roleRepository.GetAll();
            if (!role.Any())
            {
                return Enumerable.Empty<RoleDto>();
            }

            var roleDto = new List<RoleDto>();
            foreach (var roles in role)
            {
                roleDto.Add((RoleDto)roles);
            }
            return roleDto;
        }

        public RoleDto? GetByGuid(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role == null)
            {
                return null;
            }
            return (RoleDto)role;
        }

        public RoleDto? Create(NewRoleDto newRoleDto)
        {
            var role = _roleRepository.Create(newRoleDto);
            if (role == null)
            {
                return null;
            }
            return (RoleDto)role;
        }

        public int Update(RoleDto roleDto)
        {
            var role = _roleRepository.GetByGuid(roleDto.Guid);
            if (role is null)
            {
                return -1;
            }
            Role toUpdate = roleDto;
            toUpdate.CreatedDate = role.CreatedDate;
            var result = _roleRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return -1;
            }

            var result = _roleRepository.Delete(role);

            return result ? 1 : 0;
        }
    }
}
