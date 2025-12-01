using Auditing.Domain.Enums;
using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CreateFindingModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public CreateFindingModel(IHttpClientFactory http) => _http = http;

    [BindProperty]
    public FindingVm Input { get; set; } = new();

    public List<OwnerVm> OwnerList { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public async Task OnGet(int auditId)
    {
        Input.AuditId = auditId;
        Input.DetectionDate = DateTime.Today;

        var client = _http.CreateClient("api");
        OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // Regla: Severidad alta requiere responsable
        if (Input.Severity == SeverityLevel.High && Input.OwnerId == null)
        {
            ErrorMessage = "Los hallazgos de prioridad alta deben tener un responsable asignado.";
            return Page();
        }

        // Regla: Si se marca como cerrado, debe tener fecha de cierre
        if (Input.IsClosed && Input.CloseDate == null)
        {
            ErrorMessage = "Si el hallazgo está cerrado, debes indicar la fecha de cierre.";
            return Page();
        }

        var client = _http.CreateClient("api");
        try
        {
            var response = await client.PostAsJsonAsync("api/findings", Input);
            response.EnsureSuccessStatusCode();
            TempData["SuccessMessage"] = "Hallazgo creado correctamente.";
            return RedirectToPage("Index", new { auditId = Input.AuditId });
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = "Error al crear hallazgo: " + ex.Message;
            return Page();
        }
    }
}