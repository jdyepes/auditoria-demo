using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EditOwnersModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public EditOwnersModel(IHttpClientFactory http) => _http = http;

    [BindProperty]
    public OwnerVm Input { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public async Task OnGet(int id)
    {
        var client = _http.CreateClient("api");
        Input = await client.GetFromJsonAsync<OwnerVm>($"api/owners/{id}") ?? new();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _http.CreateClient("api");
        try
        {
            var response = await client.PutAsJsonAsync($"api/owners/{Input.Id}", Input);
            response.EnsureSuccessStatusCode();
            return RedirectToPage("Index");
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = "Error al actualizar responsable: " + ex.Message;
            return Page();
        }
    }
}