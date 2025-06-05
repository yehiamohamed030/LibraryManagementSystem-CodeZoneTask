using LibraryManagementSystem.DAL.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.ViewModels
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }
        [Required(ErrorMessage = "*")]
        public Genre? Genre { get; set; }
        public IEnumerable<Genre>? GenreList { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "*")]
        public int? AuthorId { get; set; }
        public IEnumerable<AuthorViewModel>? AuthorList { get; set; }
    }
}
