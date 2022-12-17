using LegoasService.Core.DAOs;

namespace LegoasService.Core.Interfaces.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        IEnumerable<Role> GetAllRoleDetail();
        Task<Role> GetByIdRoleDetail(Guid id);
        Task UpdateRole(Role role);
    }
}
