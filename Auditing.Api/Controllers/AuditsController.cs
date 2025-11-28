using Auditing.Application.DTOs;
using Auditing.Application.Services;
using Auditing.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Auditing.Api.Controllers
{
    [ApiController]
    [Route("api/audits")]
    public class AuditsController : ControllerBase
    {
        private readonly AuditAppService _service;
        public AuditsController(AuditAppService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditCreateDto dto) =>
            Created("", await _service.CreateAsync(dto));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuditUpdateDto dto) =>
            Ok(await _service.UpdateAsync(id, dto));

        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromQuery] AuditStatus status) =>
            Ok(await _service.ChangeStatusAsync(id, status));

        [HttpGet("range")]
        public async Task<IActionResult> ByRange([FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] int status) =>
            Ok(await _service.GetByDateRangeAndStatusAsync(new AuditQueryDto(start, end, status)));

        [HttpGet("owner/{ownerId:int}")]
        public async Task<IActionResult> ByOwner(int ownerId) =>
            Ok(await _service.GetByOwnerAsync(ownerId));
    }
}