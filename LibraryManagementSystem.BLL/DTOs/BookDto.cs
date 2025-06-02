using LibraryManagementSystem.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set;}
        public AuthorDto Author { get; set; }
    }
}
