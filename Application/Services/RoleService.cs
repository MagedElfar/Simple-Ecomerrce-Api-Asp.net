using AutoMapper;
using Core.DTOS.Role;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService : IRoleService
    {

        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RoleService(RoleManager<ApplicationRole> roleManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> AddRole(string name)
        {

            var role = new ApplicationRole { Name = name };
           var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                throw new BadRequestException(err: errors);
            }

            return mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await unitOfWork.Repository<ApplicationRole>().GetByIdAsync(id);

            if (role == null)
                throw new NotFoundException();

            return mapper.Map<RoleDto>(role);

        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await unitOfWork.Repository<ApplicationRole>().GetAllAsync();

            return mapper.Map<IEnumerable<RoleDto>>(roles);
        }
    }
}
