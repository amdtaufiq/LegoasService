using LegoasService.Core.DAOs;

namespace LegoasService.Core.Interfaces.Services
{
    public interface IMenuService
    {
        IEnumerable<Menu> GetAllMenu();
        Task<Menu> GetMenuById(Guid id);
        Task<bool> AddMenu(Menu menu);
        Task<bool> UpdateMenu(Guid id, Menu menu);
        Task<bool> DeleteMenu(Guid id);
    }
}
