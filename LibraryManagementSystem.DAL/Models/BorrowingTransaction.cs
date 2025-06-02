using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Models
{
    public class BorrowingTransaction
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
