using LegoasService.Core.DAOs;
using LegoasService.Core.Exceptions;
using LegoasService.Core.Interfaces.Services;
using LegoasService.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;

namespace LegoasService.Core.Services.MainServices
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        public MenuService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("Menu");
        }

        public async Task<bool> AddMenu(Menu menu)
        {
            try
            {
                await _unit.MenuRepository.Add(menu);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Menu Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteMenu(Guid id)
        {
            try
            {
                var data = await _unit.MenuRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Menu doesn't exist!");
                }
                _unit.MenuRepository.Delete(data);
                await _unit.SaveChangesAsync(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Menu Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public IEnumerable<Menu> GetAllMenu()
        {
            try
            {
                var datas = _unit.MenuRepository.GetAll();

                return datas;
            }
            catch (Exception e)
            {
                _logger.LogError("Menu List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<Menu> GetMenuById(Guid id)
        {
            try
            {
                var data = await _unit.MenuRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Menu doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("Menu By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateMenu(Guid id, Menu menu)
        {
            try
            {
                var data = await _unit.MenuRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Menu doesn't exist!");
                }

                //Set value
                data.MenuLabel = menu.MenuLabel;
                data.MenuUrl = menu.MenuUrl;
                data.Ordering = menu.Ordering;

                _unit.MenuRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Menu Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
