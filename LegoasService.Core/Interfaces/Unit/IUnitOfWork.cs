using LegoasService.Core.DAOs;
using LegoasService.Core.Interfaces.Repositories;

namespace LegoasService.Core.Interfaces.Unit
{
    public interface IUnitOfWork : IDisposable
    {
        IOfficerRepository OfficerRepository { get; }
        IRoleRepository RoleRepository { get; }
        IBaseRepository<Menu> MenuRepository { get; }
        IBaseRepository<MenuAccess> MenuAccessRepository { get; }
        IBaseRepository<Office> OfficeRepository { get; }
        IBaseRepository<OfficerRole> OfficerRoleRepository { get; }
        IBaseRepository<OfficerOffice> OfficerOfficeRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync(bool delete = false);
    }
}
