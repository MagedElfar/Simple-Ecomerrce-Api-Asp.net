using Core.DTOS.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRolesAsync();

        Task<RoleDto> GetByIdAsync(int id);

        Task<RoleDto> AddRole(string name);
    }
}
