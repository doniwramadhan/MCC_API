using APIMCC.DTOs.Rooms;
using APIMCC.Models;

namespace APIMCC.DTOs.Roles
{
    public class NewRoleDto
    {
        public string Name { get; set; }

        public static implicit operator Role(NewRoleDto newRoleDto)
        {
            return new Role
            {
                Guid = new Guid(),
                Name = newRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }
    }
}
