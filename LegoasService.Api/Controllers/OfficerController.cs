using AutoMapper;
using LegoasService.Core.CustomEntities;
using LegoasService.Core.DAOs;
using LegoasService.Core.DTOs;
using LegoasService.Core.Filters;
using LegoasService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LegoasService.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfficerController : ControllerBase
    {
        private readonly IOfficerService _officerService;
        private readonly IMapper _mapper;

        public OfficerController(
            IOfficerService officerService,
            IMapper mapper)
        {
            _officerService = officerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOfficer([FromQuery] OfficerFilter filter)
        {
            var officers = _officerService.GetAllOfficer(filter);
            var metaData = new Metadata()
            {
                TotalCount = officers.TotalCount,
                PageSize = officers.PageSize,
                CurrentPage = officers.CurrentPage,
                TotalPages = officers.TotalPages,
                HasNextPage = officers.HasNextPage,
                HasPreviousPage = officers.HasPreviousPage
            };
            var officerDtos = _mapper.Map<IEnumerable<OfficerResponse>>(officers);

            var response = new ApiResponse<IEnumerable<OfficerResponse>>(officerDtos)
            {
                Message = new Message
                {
                    Description = "list data officer"
                },
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfficerByID(Guid id)
        {
            var officer = await _officerService.GetOfficerById(id);
            var officerDto = _mapper.Map<OfficerResponse>(officer);

            var response = new ApiResponse<OfficerResponse>(officerDto)
            {
                Message = new Message
                {
                    Description = "Detail data officer"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOfficer(CreateOfficerRequest request)
        {
            var officerMap = _mapper.Map<Officer>(request);
            var result = await _officerService.AddOfficer(officerMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create officer"
                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOfficer(Guid id, UpdateOfficerRequest request)
        {
            var officerMap = _mapper.Map<Officer>(request);
            var result = await _officerService.UpdateOfficer(id, officerMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update officer"
                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficer(Guid id)
        {
            var result = await _officerService.DeleteOfficer(id);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success delete officer"
                }
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginOfficer(LoginRequest request)
        {
            var result = await _officerService.LoginOfficer(request);

            var response = new ApiResponse<LoginResponse>(result)
            {
                Message = new Message
                {
                    Description = "Success login"
                }
            };

            return Ok(response);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> ProfileOfficer()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string[] tokens = token.ToString().Split(' ');

            var officer = await _officerService.GetOfficerByToken(tokens[1]);
            var officerDto = _mapper.Map<OfficerResponse>(officer);

            var response = new ApiResponse<OfficerResponse>(officerDto);

            return Ok(response);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePasswordOfficer(UpdatePasswordRequest request)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string[] tokens = token.ToString().Split(' ');

            var result = await _officerService.UpdatePasswordOfficer(tokens[1], request);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success Update Password"
                }
            };

            return Ok(response);
        }
    }
}
