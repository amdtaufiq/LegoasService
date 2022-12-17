using LegoasService.Core.DAOs;

namespace LegoasService.Core.Interfaces.Repositories
{
    public interface IOfficerRepository : IBaseRepository<Officer>
    {
        IEnumerable<Officer> GetAllOfficerDetail();
        Task<Officer> GetByIdOfficerDetail(Guid id);
        Task UpdateOfficer(Officer role);
        Task<Officer> GetOfficerByEmail(string email);
        Task<Officer> GetOfficerByToken(string token);
    }
}
