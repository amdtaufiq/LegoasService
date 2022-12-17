using LegoasService.Core.CustomEntities;
using LegoasService.Core.CustomEntities.Options;
using LegoasService.Core.DAOs;
using LegoasService.Core.DTOs;
using LegoasService.Core.Exceptions;
using LegoasService.Core.Filters;
using LegoasService.Core.Interfaces.Services;
using LegoasService.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LegoasService.Core.Services.MainServices
{
    public class OfficerService : IOfficerService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly PaginationOptions _paginationOptions;

        public OfficerService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory,
            IPasswordService passwordService,
            ITokenService tokenService,
            IOptions<PaginationOptions> paginationOptions)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("Officer");
            _passwordService = passwordService;
            _tokenService = tokenService;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<bool> AddOfficer(Officer officer)
        {
            var cekEmail = _unit.OfficerRepository.GetOfficerByEmail(officer.Email);
            if (cekEmail != null)
            {
                throw new UnprocessableEntityException("email already exists");
            }
            try
            {
                officer.Password = _passwordService.Hash(officer.Password);

                await _unit.OfficerRepository.Add(officer);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Officer Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteOfficer(Guid id)
        {
            try
            {
                var data = await _unit.OfficerRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Officer doesn't exist!");
                }
                _unit.OfficerRepository.Delete(data);
                await _unit.SaveChangesAsync(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Officer Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public PagedList<Officer> GetAllOfficer(OfficerFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            try
            {
                var datas = _unit.OfficerRepository.GetAllOfficerDetail();

                return PagedList<Officer>.Create(datas, filters.PageNumber, filters.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError("Officer List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<Officer> GetOfficerById(Guid id)
        {
            try
            {
                var data = await _unit.OfficerRepository.GetByIdOfficerDetail(id);
                if (data == null)
                {
                    throw new NotFoundException("Officer doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("Officer By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateOfficer(Guid id, Officer officer)
        {
            try
            {
                var data = await _unit.OfficerRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Officer doesn't exist!");
                }

                _unit.OfficerRepository.UpdateOfficer(officer);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Officer Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<Officer> GetOfficerByToken(string token)
        {
            try
            {
                var data = await _unit.OfficerRepository.GetOfficerByToken(token);
                if (data == null)
                {
                    throw new NotFoundException("Officer doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("Officer By Token => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<LoginResponse> LoginOfficer(LoginRequest request)
        {
            var officer = await _unit.OfficerRepository.GetOfficerByEmail(request.Email);
            if (officer == null)
            {
                throw new NotFoundException("Officer doesn't exist!");
            }
            var isValidPassword = _passwordService.Check(officer.Password, request.Password);
            if (isValidPassword == false)
            {
                throw new UnprocessableEntityException("Your password is wrong");
            }

            try
            {
                var result = _tokenService.GenerateTokenOfficer(officer);

                //update token
                officer.Token = result.Token;
                _unit.OfficerRepository.Update(officer);
                await _unit.SaveChangesAsync();

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("Officer Login => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdatePasswordOfficer(string token, UpdatePasswordRequest request)
        {
            var officer = await _unit.OfficerRepository.GetOfficerByToken(token);
            if (token == null)
            {
                throw new NotFoundException("Officer doesn't exist!");
            }

            var isValidPassword = _passwordService.Check(officer.Password, request.CurrentPassword);
            if (isValidPassword == false)
            {
                throw new UnprocessableEntityException("Your password is wrong");
            }

            try
            {
                //update password
                officer.Password = _passwordService.Hash(request.Password);
                _unit.OfficerRepository.Update(officer);
                await _unit.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Officer Update Password => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
