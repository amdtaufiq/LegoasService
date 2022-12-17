using LegoasService.Core.DAOs;

namespace LegoasService.Core.Interfaces.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRole();
        Task<Role> GetRoleById(Guid id);
        Task<bool> AddRole(Role role);
        Task<bool> UpdateRole(Guid id, Role role);
        Task<bool> DeleteRole(Guid id);
        Task<bool> RefreshMenuAccess();
    }
}
