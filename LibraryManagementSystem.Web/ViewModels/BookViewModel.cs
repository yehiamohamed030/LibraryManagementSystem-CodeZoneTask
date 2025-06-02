using LibraryManagementSystem.DAL.Enums;
using LibraryManagementSystem.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }
        [Required(ErrorMessage = "*")]
        public Genre Genre { get; set; }
        public string? Description { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public AuthorViewModel Author { get; set; }
    }
}
