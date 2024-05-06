using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using School.Data.Constants;
using School.Data.Entities;
using School.Data.Requests;
using School.Service.IService;

namespace School.Service.ServiceImp
{
    public class AuthorizationService(
        IConfiguration configuration,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
        ) : IAuthorizationService
    {
        private readonly IConfiguration configuration = configuration;
        private readonly UserManager<User> userManager = userManager;
        private readonly RoleManager<IdentityRole> roleManager = roleManager;
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var result = await roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
                return State.Success;
            return State.Failed;
        }

        public async Task<string> DeleteRoleAsync(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null) return State.Failed;
            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded) return State.Success;
            return State.Failed;
        }

        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var role = await roleManager.FindByIdAsync(request.Id);
            if (role == null) return State.Failed;

            role.Name = request.Name;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded) return State.Success;
            return State.Failed;
        }

        public async Task<IdentityRole> GetRoleById(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }

        public async Task<List<IdentityRole>> GetRolesList()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<IQueryable> GetRolesListQueryable()
        {
            return roleManager.Roles.AsQueryable();
        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return (List<string>)await userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsRoleExistById(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null) return false;
            else return true;
        }

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null) return false;
            return true;
        }
    }
}
