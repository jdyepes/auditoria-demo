using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AuditsIndexModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public AuditsIndexModel(IHttpClientFactory http) => _http = http;

    public List<AuditVm> Items { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public DateTime? Start { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? End { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? Status { get; set; }


    public async Task OnGet(DateTime? start, DateTime? end, int? status)
    {
        var client = _http.CreateClient("api");

        var now = DateTime.Today;
        var firstDay = new DateTime(now.Year, now.Month, 1);
        var lastDay = firstDay.AddMonths(1).AddDays(-1);

        var startDate = start ?? firstDay;
        var endDate = end ?? lastDay;

        var url = $"api/audits/range?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}&status={(status ?? 0)}";
        Items = await client.GetFromJsonAsync<List<AuditVm>>(url) ?? new();
    }   
}