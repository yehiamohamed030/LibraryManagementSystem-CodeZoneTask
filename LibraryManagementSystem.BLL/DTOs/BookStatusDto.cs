using LibraryManagementSystem.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.DTOs
{
    public class BookStatusDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
