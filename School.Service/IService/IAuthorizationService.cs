using Microsoft.AspNetCore.Identity;
using School.Data.Requests;

namespace School.Service.IService
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(string roleId);
        public Task<bool> IsRoleExistById(string roleId);

        public Task<IQueryable> GetRolesListQueryable();
        public Task<List<IdentityRole>> GetRolesList();

        public Task<IdentityRole> GetRoleById(string id);

        public Task<List<string>> GetUserRoles(string userId);

        //public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        //public Task<ManageUserClaimsResult> ManageUserClaimData(User user);
        //public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);
    }
}
