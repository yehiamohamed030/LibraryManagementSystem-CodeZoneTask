using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.ViewModels
{
    public class CreateBorrowBookViewModel
    {
        public IEnumerable<BorrowingBookViewModel> Books { get; set; }
        [Required(ErrorMessage = "Please select a book.")]
        public int bookId { get; set; }
    }
}
