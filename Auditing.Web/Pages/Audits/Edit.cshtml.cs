using Auditing.Domain.Enums;
using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EditAuditsModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public EditAuditsModel(IHttpClientFactory http) => _http = http;

    [BindProperty]
    public AuditVm Input { get; set; } = new();

    public List<OwnerVm> OwnerList { get; set; } = new();
    public string? ErrorMessage { get; set; }
    public bool IsEditable => Input.Status == (int)AuditStatus.Pending;

    public async Task<IActionResult> OnGet(int id)
    {
        var client = _http.CreateClient("api");

        Input = await client.GetFromJsonAsync<AuditVm>($"api/audits/{id}")
            ?? throw new Exception("No se pudo cargar la auditoría.");

        OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _http.CreateClient("api");

        if (!ModelState.IsValid)
        {
            OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();
            return Page();
        }

        if (Input.StartDate >= Input.EndDate)
        {
            ErrorMessage = "La fecha de inicio debe ser menor que la de fin.";
            OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();
            return Page();
        }

        var response = await client.PutAsJsonAsync($"api/audits/{Input.Id}", Input);

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = await response.Content.ReadAsStringAsync();
            OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();
            return Page();
        }

        TempData["SuccessMessage"] = "Auditoría actualizada correctamente.";
        return RedirectToPage("Edit", new { id = Input.Id });
    }

    public async Task<IActionResult> OnPostChangeStatusAsync(int id)
    {
        var client = _http.CreateClient("api");

        var audit = await client.GetFromJsonAsync<AuditVm>($"api/audits/{id}");
        if (audit == null) return NotFound();

        var response = await client.PatchAsync(
            $"api/audits/{id}/status?status={(int)audit.Status}", null);

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = "No se pudo cambiar el estado.";
            OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();
            return Page();
        }

        Input = await client.GetFromJsonAsync<AuditVm>($"api/audits/{id}")
            ?? throw new Exception("No se pudo recargar la auditoría.");

        OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();
        TempData["SuccessMessage"] = $"Estado actualizado a {Input.Status}.";
        return RedirectToPage("Edit", new { id });
    }
}