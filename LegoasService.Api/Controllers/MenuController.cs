using AutoMapper;
using LegoasService.Core.CustomEntities;
using LegoasService.Core.DAOs;
using LegoasService.Core.DTOs;
using LegoasService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegoasService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuController(
            IMenuService menuService,
            IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllMenu()
        {
            var menus = _menuService.GetAllMenu();
            var menuDtos = _mapper.Map<IEnumerable<MenuResponse>>(menus);

            var response = new ApiResponse<IEnumerable<MenuResponse>>(menuDtos)
            {
                Message = new Message
                {
                    Description = "List data Menu"
                }
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuByID(Guid id)
        {
            var menu = await _menuService.GetMenuById(id);
            var menuDto = _mapper.Map<MenuResponse>(menu);

            var response = new ApiResponse<MenuResponse>(menuDto)
            {
                Message = new Message
                {
                    Description = "Detail data Menu"
                }
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateMenu(CreateMenuRequest request)
        {
            var menuMap = _mapper.Map<Menu>(request);
            var result = await _menuService.AddMenu(menuMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create Menu"

                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(Guid id, UpdateMenuRequest request)
        {
            var menuMap = _mapper.Map<Menu>(request);
            var result = await _menuService.UpdateMenu(id, menuMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update Menu"

                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            var result = await _menuService.DeleteMenu(id);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success delete Menu"
                }
            };

            return Ok(response);
        }
    }
}
