using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^(\b\w{2,}\b\s){3}\b\w{2,}\b$", ErrorMessage = "Full name must be four words, each with at least 2 characters.")]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Website { get; set; }
        [MaxLength(300)]
        public string? Bio { get; set; }

        public IReadOnlyCollection<Book> Books => _books.AsReadOnly();
        private List<Book> _books { get; set; } = new List<Book>();

    }
}
