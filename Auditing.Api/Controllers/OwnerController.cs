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
        public async Task<IActionResult> Create([FromBody] OwnerCreateDto dto) =>
            Created("", await _service.CreateAsync(dto));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OwnerUpdateDto dto) =>
            Ok(await _service.UpdateAsync(id, dto));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _service.GetByIdAsync(id));
    }
}