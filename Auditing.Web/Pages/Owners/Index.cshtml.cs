using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class OwnersIndexModel : PageModel
{
    private readonly IHttpClientFactory _http;
    public OwnersIndexModel(IHttpClientFactory http) => _http = http;

    public List<OwnerVm> Items { get; set; } = new();

    public async Task OnGet()
    {
        var client = _http.CreateClient("api");
        Items = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();
    }
}