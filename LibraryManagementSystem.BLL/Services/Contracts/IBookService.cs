using LibraryManagementSystem.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> ListBooksAsync();
        Task<PaginatedBookDto> GetPaginatedBooksAsync(int pageNumber, int pageSize);

        Task<BookDto> GetBookByIdAsync(int bookId);

        Task AddBookAsync(BookDto book);

        Task UpdateBookAsync(int bookId, BookDto book);

        Task DeleteBookAsync(int bookId);
    }
}
