using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class FindingsIndexModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public FindingsIndexModel(IHttpClientFactory http) => _http = http;

    public int? AuditId { get; set; }
    public int? Severity { get; set; }
    public List<FindingVm> Items { get; set; } = new();
    public List<AuditVm> Audits { get; set; } = new();

    public async Task OnGet(int? auditId, int? severity)
    {
        AuditId = auditId;
        Severity = severity;

        var client = _http.CreateClient("api");

        // Traemos todas las auditorías registradas para el dropdown
        Audits = await client.GetFromJsonAsync<List<AuditVm>>("api/audits") ?? new();

        if (auditId.HasValue)
        {
            string url = severity.HasValue
                ? $"api/findings/by-audit/{auditId}?severity={severity.Value}"
                : $"api/findings/by-audit/{auditId}";

            Items = await client.GetFromJsonAsync<List<FindingVm>>(url) ?? new();
        }
        else
        {
            Items = new List<FindingVm>();
        }
    }
}