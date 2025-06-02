using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^(\b\w{2,}\b\s){3}\b\w{2,}\b$", ErrorMessage = "Full name must be four words, each with at least 2 characters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        public string? Website { get; set; }
        [MaxLength(300)]
        public string? Bio { get; set; }
        public IReadOnlyList<BookViewModel>? Books { get; set; }
    }
}
