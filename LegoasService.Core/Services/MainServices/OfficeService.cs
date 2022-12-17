using LegoasService.Core.DAOs;
using LegoasService.Core.Exceptions;
using LegoasService.Core.Interfaces.Services;
using LegoasService.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;

namespace LegoasService.Core.Services.MainServices
{
    public class OfficeService : IOfficeService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        public OfficeService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("Office");
        }

        public async Task<bool> AddOffice(Office office)
        {
            try
            {
                await _unit.OfficeRepository.Add(office);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Office Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteOffice(Guid id)
        {
            try
            {
                var data = await _unit.OfficeRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Office doesn't exist!");
                }
                _unit.OfficeRepository.Delete(data);
                await _unit.SaveChangesAsync(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Office Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public IEnumerable<Office> GetAllOffice()
        {
            try
            {
                var datas = _unit.OfficeRepository.GetAll();

                return datas;
            }
            catch (Exception e)
            {
                _logger.LogError("Office List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<Office> GetOfficeById(Guid id)
        {
            try
            {
                var data = await _unit.OfficeRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Office doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("Office By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateOffice(Guid id, Office office)
        {
            try
            {
                var data = await _unit.OfficeRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Office doesn't exist!");
                }

                //Set value
                data.OfficeName = office.OfficeName;
                data.Address = office.Address;
                data.Contact = office.Contact;

                _unit.OfficeRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Office Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
