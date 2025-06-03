using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.BLL.Services.Contracts;
using LibraryManagementSystem.DAL.Data;
using LibraryManagementSystem.DAL.Enums;
using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services.Implementions
{
    public class BorrowingService : IBorrowingService
    {
        private readonly AppDbContext _context;
        public BorrowingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookStatusDto>> ListBooksWithStatusAsync()
        {
            return await _context.Books.Select(b => new BookStatusDto
            {
                BookId = b.Id,
                Title = b.Title,
                Status = _context.BorrowingTransactions.Any(bt => bt.BookId == b.Id && bt.IsReturned == false)
                     ? Status.Borrowed : Status.Available,
                BorrowedDate = _context.BorrowingTransactions
                                      .Where(bt => bt.BookId == b.Id && bt.IsReturned == false)
                                      .Select(bt => (DateTime?)bt.BorrowedDate) 
                                      .FirstOrDefault(),
            }).ToListAsync();
        }
        public async Task BorrowBookAsync(int bookId)
        {
            if (!await IsBookAvailableAsync(bookId))
                throw new Exception("Book is already borrowed");

            _context.BorrowingTransactions.Add(new BorrowingTransaction
            {
                BookId = bookId,
                BorrowedDate = DateTime.UtcNow,
                IsReturned = false
            });

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookStatusDto>> FilterBooksAsync(Status? status = null, DateTime? borrowDate = null, DateTime? returnDate = null)
        {
            var query = _context.BorrowingTransactions.Include(bt => bt.Book).AsQueryable();

            if (status == Status.Borrowed)
                query = query.Where(bt => bt.ReturnedDate == null);
            else if (status == Status.Available)
                query = query.Where(bt => bt.ReturnedDate != null);

            if (borrowDate.HasValue)
                query = query.Where(bt => bt.BorrowedDate.Date == borrowDate.Value.Date);

            if (returnDate.HasValue)
                query = query.Where(bt => bt.ReturnedDate.HasValue && bt.ReturnedDate.Value.Date == returnDate.Value.Date);

            return await query.Select(bt => new BookStatusDto
            {
                BookId = bt.BookId,
                Title = bt.Book.Title,
                Status = bt.ReturnedDate == null ? Status.Borrowed : Status.Available,
                BorrowedDate = bt.BorrowedDate,
                ReturnedDate = bt.ReturnedDate
            }).ToListAsync();
        }
        public async Task ReturnBookAsync(int bookId)
        {
            var borrow = await _context.BorrowingTransactions
            .Where(bt => bt.BookId == bookId && bt.ReturnedDate == null)
            .FirstOrDefaultAsync();

            if (borrow == null)
                throw new Exception("Book is not borrowed");

            borrow.ReturnedDate = DateTime.UtcNow;
            borrow.IsReturned = true;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            return !await _context.BorrowingTransactions.AnyAsync(bt => bt.BookId == bookId && bt.ReturnedDate == null);
        }
    }
}
