using LibraryManagementSystem.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.ViewModels
{
    public class BorrowingBookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        [Display(Name = "Borrowed Date")]
        [DataType(DataType.Date)]
        public DateTime? BorrowedDate { get; set; }

        [Display(Name = "Returned Date")]
        [DataType(DataType.Date)]
        public DateTime? ReturnedDate { get; set; }
    }
}
