using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EditFindingModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public EditFindingModel(IHttpClientFactory http) => _http = http;

    [BindProperty]
    public FindingVm Input { get; set; } = new();

    public List<OwnerVm> OwnerList { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var client = _http.CreateClient("api");

        var finding = await client.GetFromJsonAsync<FindingVm>($"api/findings/{id}");
        if (finding == null) return NotFound();

        Input = finding;
        OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // Regla: Severidad alta requiere responsable
        if (Input.Severity == 2 && Input.OwnerId == null)
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
            var response = await client.PutAsJsonAsync($"api/findings/{Input.Id}", Input);
            response.EnsureSuccessStatusCode();
            return RedirectToPage("Index", new { auditId = Input.AuditId });
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = "Error al actualizar hallazgo: " + ex.Message;
            return Page();
        }
    }
}