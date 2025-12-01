using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CreateOwnersModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public CreateOwnersModel(IHttpClientFactory http) => _http = http;

    [BindProperty]
    public OwnerVm Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var client = _http.CreateClient("api");

        try
        {
            var response = await client.PostAsJsonAsync("api/owners", Input);
            response.EnsureSuccessStatusCode();
            return RedirectToPage("Index");
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = "Error al crear responsable: " + ex.Message;
            return Page();
        }
    }
}
