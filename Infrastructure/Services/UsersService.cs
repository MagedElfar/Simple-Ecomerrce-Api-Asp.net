using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<ApplicationUser> GetUserByIdAsync(int id)
        {

            return await userManager.FindByIdAsync(id.ToString());

        }
    }
}
