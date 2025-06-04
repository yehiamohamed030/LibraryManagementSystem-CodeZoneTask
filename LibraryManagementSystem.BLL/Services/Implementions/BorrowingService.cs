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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                                      .OrderByDescending(bt => bt.BorrowedDate)
                                      .Select(bt => (DateTime?)bt.BorrowedDate)
                                      .FirstOrDefault(),
                ReturnedDate = _context.BorrowingTransactions
                                      .Where(bt => bt.BookId == b.Id && bt.IsReturned == true)
                                      .OrderByDescending(bt => bt.ReturnedDate)
                                      .Select(bt => (DateTime?)bt.ReturnedDate)
                                      .FirstOrDefault()
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
            var query = _context.Books.Select(b => new BookStatusDto
            {
                BookId = b.Id,
                Title = b.Title,
                Status = _context.BorrowingTransactions.Any(bt => bt.BookId == b.Id && bt.IsReturned == false)
                     ? Status.Borrowed : Status.Available,
                BorrowedDate = _context.BorrowingTransactions
                                      .Where(bt => bt.BookId == b.Id && bt.IsReturned == false)
                                      .OrderByDescending(bt => bt.BorrowedDate)
                                      .Select(bt => (DateTime?)bt.BorrowedDate)
                                      .FirstOrDefault(),
                ReturnedDate = _context.BorrowingTransactions
                                      .Where(bt => bt.BookId == b.Id && bt.IsReturned == true)
                                      .OrderByDescending(bt => bt.ReturnedDate)
                                      .Select(bt => (DateTime?)bt.ReturnedDate)
                                      .FirstOrDefault()
            });

            if (status.HasValue)
            {
                if (status == Status.Borrowed)
                    query = query.Where(bt => bt.Status == Status.Borrowed);
                else if (status == Status.Available)
                    query = query.Where(bt => bt.Status == Status.Available);
            }

            if (borrowDate.HasValue)
            {
                var date = borrowDate.Value.Date;
                var nextDate = date.AddDays(1);
                query = query.Where(bt => bt.BorrowedDate >= date && bt.BorrowedDate < nextDate);
            }
            if (returnDate.HasValue)
            {
                var date = returnDate.Value.Date;
                var nextDate = date.AddDays(1);
                query = query.Where(bt => bt.ReturnedDate >= date && bt.ReturnedDate < nextDate);
            }

            return await query.ToListAsync();
        }
        public async Task ReturnBookAsync(int bookId)
        {
            var borrow = await _context.BorrowingTransactions
                                       .Where(bt => bt.BookId == bookId && !bt.IsReturned)
                                       .OrderByDescending(bt => bt.BorrowedDate)
                                       .FirstOrDefaultAsync();

            if (borrow == null)
                throw new Exception("Book is not borrowed");

            borrow.ReturnedDate = DateTime.UtcNow;
            borrow.IsReturned = true;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var latestTransaction = await _context.BorrowingTransactions
                                                  .Where(bt => bt.BookId == bookId)
                                                  .OrderByDescending(bt => bt.BorrowedDate)
                                                  .FirstOrDefaultAsync();
            return latestTransaction == null || latestTransaction.IsReturned;

        }
    }
}
