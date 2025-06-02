using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services.Contracts
{
    public interface IBorrowingService
    {
        Task<IEnumerable<BookStatusDto>> ListBooksWithStatusAsync();

        Task<IEnumerable<BookStatusDto>> FilterBooksAsync(
            Status? status = null,
            DateTime? borrowDate = null,
            DateTime? returnDate = null);

        Task BorrowBookAsync(int bookId);

        Task ReturnBookAsync(int bookId);

        Task<bool> IsBookAvailableAsync(int bookId);
    }
}
