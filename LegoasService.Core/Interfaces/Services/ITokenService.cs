using LegoasService.Core.DAOs;
using LegoasService.Core.DTOs;

namespace LegoasService.Core.Interfaces.Services
{
    public interface ITokenService
    {
        LoginResponse GenerateTokenOfficer(Officer officer);
    }
}
