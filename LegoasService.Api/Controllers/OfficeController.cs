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
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;

        public OfficeController(
            IOfficeService officeService,
            IMapper mapper)
        {
            _officeService = officeService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOffice()
        {
            var offices = _officeService.GetAllOffice();
            var officeDtos = _mapper.Map<IEnumerable<OfficeResponse>>(offices);

            var response = new ApiResponse<IEnumerable<OfficeResponse>>(officeDtos)
            {
                Message = new Message
                {
                    Description = "List data Office"
                }
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfficeByID(Guid id)
        {
            var office = await _officeService.GetOfficeById(id);
            var officeDto = _mapper.Map<OfficeResponse>(office);

            var response = new ApiResponse<OfficeResponse>(officeDto)
            {
                Message = new Message
                {
                    Description = "Detail data Office"
                }
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateOffice(CreateOfficeRequest request)
        {
            var officeMap = _mapper.Map<Office>(request);
            var result = await _officeService.AddOffice(officeMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create Office"

                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffice(Guid id, UpdateOfficeRequest request)
        {
            var officeMap = _mapper.Map<Office>(request);
            var result = await _officeService.UpdateOffice(id, officeMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update Office"

                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffice(Guid id)
        {
            var result = await _officeService.DeleteOffice(id);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success delete Office"
                }
            };

            return Ok(response);
        }
    }
}
