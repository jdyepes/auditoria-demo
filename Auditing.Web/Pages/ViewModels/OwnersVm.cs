using System.ComponentModel.DataAnnotations;

namespace Auditing.Web.Pages.ViewModels
{
    public class OwnerVm
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;
        [Required, EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Area { get; set; } = default!;
    }
}
