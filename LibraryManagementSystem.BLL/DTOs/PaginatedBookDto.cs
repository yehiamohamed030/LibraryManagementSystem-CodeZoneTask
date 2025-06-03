﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.DTOs
{
    public class PaginatedBookDto
    {
        public IEnumerable<BookDto> Books { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int pageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
