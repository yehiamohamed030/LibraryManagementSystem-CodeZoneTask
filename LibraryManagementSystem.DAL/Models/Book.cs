using LibraryManagementSystem.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public ICollection<BorrowingTransaction>? BorrowingTransactions { get; set; } = new List<BorrowingTransaction>();
    
    }
}
