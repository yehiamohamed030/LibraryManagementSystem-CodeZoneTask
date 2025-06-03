using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Web.ViewModels
{
    public class CreateBorrowBookViewModel
    {
        public IEnumerable<SelectListItem> Books { get; set; }
        public int bookId { get; set; }
    }
}
