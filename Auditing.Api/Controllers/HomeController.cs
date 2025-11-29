using Auditing.Application.DTOs;
using Auditing.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auditing.Api.Controllers
{
    [ApiController]
    [Route("api/findings")]
    public class FindingsController : ControllerBase
    {
        private readonly FindingAppService _service;
        public FindingsController(FindingAppService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FindingCreateDto dto) =>
            Created("", await _service.CreateAsync(dto));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FindingUpdateDto dto) =>
            Ok(await _service.UpdateAsync(id, dto));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _service.GetByIdAsync(id));

        [HttpGet("audit/{auditId:int}/severity/{severity:int}")]
        public async Task<IActionResult> GetByAuditAndSeverity(int auditId, int severity) =>
            Ok(await _service.GetByAuditAndSeverityAsync(auditId, severity));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}