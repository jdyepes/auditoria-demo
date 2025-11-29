using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

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

public class OwnerVm
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Area { get; set; } = default!;
}