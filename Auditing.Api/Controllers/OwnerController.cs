using Auditing.Application.DTOs;
using Auditing.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auditing.Api.Controllers
{
    [ApiController]
    [Route("api/owners")]
    public class OwnersController : ControllerBase
    {
        private readonly OwnerAppService _service;
        public OwnersController(OwnerAppService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OwnerCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest("Los campos Nombre/ Email son obligaorios");
            }

            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OwnerUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Area))
                return BadRequest("Los campos Nombre/ Email son obligaorios");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var owners = await _service.GetAllAsync();
            return Ok(owners);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var owner = await _service.GetByIdAsync(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

    }
}