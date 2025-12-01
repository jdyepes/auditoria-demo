using Auditing.Application.DTOs;
using Auditing.Application.Services;
using Auditing.Domain.Entities;
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
        public async Task<ActionResult> Create(FindingCreateDto dto)
        {
            var finding = await _service.CreateAsync(dto);
            return Ok(new { message = "Hallazgo creado correctamente" });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, FindingUpdateDto dto)
        {
            var finding = await _service.UpdateAsync(id, dto);
            return Ok(new { message = "Hallazgo actualizado correctamente" });
        }

        [HttpGet("by-audit/{auditId:int}")]
        public async Task<ActionResult> GetByAudit(int auditId, [FromQuery] int? severity)
        {
            var findings = await _service.GetByAuditAndSeverityAsync(auditId, severity ?? -1);
            return Ok(findings);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Finding?>> GetById(int id)
        {
            var finding = await _service.GetByIdAsync(id);
            if (finding == null) return NotFound();
            return Ok(finding);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}