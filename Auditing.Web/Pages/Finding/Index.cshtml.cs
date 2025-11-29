using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

public class FindingsIndexModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public FindingsIndexModel(IHttpClientFactory http) => _http = http;

    public int? AuditId { get; set; }
    public int? Severity { get; set; }
    public List<FindingVm> Items { get; set; } = new();

    public async Task OnGet(int? auditId, int? severity)
    {
        AuditId = auditId;
        Severity = severity;

        var client = _http.CreateClient("api");

        if (auditId.HasValue && severity.HasValue)
        {
            Items = await client.GetFromJsonAsync<List<FindingVm>>(
                $"api/findings/audit/{auditId}/severity/{severity}") ?? new();
        }
        else if (auditId.HasValue)
        {
            // fallback: traer todos los hallazgos de esa auditoría
            Items = await client.GetFromJsonAsync<List<FindingVm>>(
                $"api/findings/audit/{auditId}/severity/0") ?? new();
        }
    }
}

public class FindingVm
{
    public string Description { get; set; } = default!;
    public string Type { get; set; } = default!;
    public int Severity { get; set; }
    public DateTime DetectionDate { get; set; }
}