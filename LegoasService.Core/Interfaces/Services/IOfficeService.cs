using LegoasService.Core.DAOs;

namespace LegoasService.Core.Interfaces.Services
{
    public interface IOfficeService
    {
        IEnumerable<Office> GetAllOffice();
        Task<Office> GetOfficeById(Guid id);
        Task<bool> AddOffice(Office office);
        Task<bool> UpdateOffice(Guid id, Office office);
        Task<bool> DeleteOffice(Guid id);
    }
}
