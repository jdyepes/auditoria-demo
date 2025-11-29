using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

public class AuditsIndexModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public AuditsIndexModel(IHttpClientFactory http) => _http = http;

    public List<AuditVm> Items { get; set; } = new();

    public async Task OnGet(DateTime? start, DateTime? end, int? status)
    {
        var client = _http.CreateClient("api");
        var url = $"api/audits/range?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}&status={(status ?? 0)}";
        Items = await client.GetFromJsonAsync<List<AuditVm>>(url) ?? new();
    }
}

public class AuditVm
{
    public string Title { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AuditedArea { get; set; } = default!;
    public int Status { get; set; }
    public OwnerVm? Owner { get; set; }
}
