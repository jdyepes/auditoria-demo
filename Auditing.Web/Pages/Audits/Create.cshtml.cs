using Auditing.Web.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auditing.Web.Pages.Audits
{
    public class CreateAuditsModel : PageModel
    {
        private readonly IHttpClientFactory _http;
        public CreateAuditsModel(IHttpClientFactory http) => _http = http;

        [BindProperty]
        public AuditVm Input { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public List<OwnerVm> OwnerList { get; set; } = new();
        public List<string> AreaList { get; set; } = new();


        public async Task OnGet(int? id)
        {
            var client = _http.CreateClient("api");
            // Fecha actual como inicio
            Input.StartDate = DateTime.Today;

            // Fecha de fin por defecto: 7 días después
            Input.EndDate = DateTime.Today.AddDays(1);

            OwnerList = await client.GetFromJsonAsync<List<OwnerVm>>("api/owners") ?? new();

            // Cargar áreas únicas desde los responsables
            AreaList = OwnerList
                .Select(o => o.Area)
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .Distinct()
                .OrderBy(a => a)
                .ToList();

            if (id.HasValue)
                Input = await client.GetFromJsonAsync<AuditVm>($"api/audits/{id}") ?? new();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.StartDate >= Input.EndDate)
            {
                ErrorMessage = "La fecha de inicio debe ser menor que la de fin.";
                return Page();
            }

            var client = _http.CreateClient("api");
            try
            {
                var response = await client.PostAsJsonAsync("api/audits", Input);
                response.EnsureSuccessStatusCode();
                TempData["SuccessMessage"] = "Auditoría guardada exitosamente.";

                return RedirectToPage("Index");

            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = "Error al guardar auditoría: " + ex.Message;
                return Page();
            }
        }

    }
}
