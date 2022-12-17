using LegoasService.Core.CustomEntities;
using LegoasService.Core.DAOs;
using LegoasService.Core.DTOs;
using LegoasService.Core.Filters;

namespace LegoasService.Core.Interfaces.Services
{
    public interface IOfficerService
    {
        PagedList<Officer> GetAllOfficer(OfficerFilter filters);
        Task<Officer> GetOfficerById(Guid id);
        Task<bool> AddOfficer(Officer officer);
        Task<bool> UpdateOfficer(Guid id, Officer officer);
        Task<bool> DeleteOfficer(Guid id);
        Task<Officer> GetOfficerByToken(string token);
        Task<LoginResponse> LoginOfficer(LoginRequest request);
        Task<bool> UpdatePasswordOfficer(string token, UpdatePasswordRequest request);
    }
}
