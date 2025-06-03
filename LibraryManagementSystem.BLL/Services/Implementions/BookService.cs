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
    public class BookService: IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BookDto>> ListBooksAsync()
        {
            return await _context.Books.Include(b => b.Author)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Genre = b.Genre,
                    Description = b.Description,
                    Author = new AuthorDto { Id = b.Author.Id, FullName = b.Author.FullName }
                }).ToListAsync();
        }
        public async Task<PaginatedBookDto> GetPaginatedBooksAsync(int pageNumber, int pageSize)
        {
            var totalRecords = _context.Books.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var books = await _context.Books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Genre = b.Genre,
                    Description = b.Description,
                    Author = new AuthorDto { Id = b.Author.Id, FullName = b.Author.FullName }
                }).ToListAsync();
            var viewModel = new PaginatedBookDto
            {
                Books = books,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                pageSize = pageSize
            };
            return viewModel;
        }
        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
            var book = await _context.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null) throw new Exception("Book not found");

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                Description = book.Description,
                AuthorId = book.Author.Id,
                Author = new AuthorDto { Id = book.Author.Id, FullName = book.Author.FullName }
            };
        }
        public async Task AddBookAsync(BookDto dto)
        {
            ValidateBook(dto);

            if (!await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId))
                throw new Exception("Author not found");

            var book = new Book
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Description = dto.Description,
                AuthorId = dto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBookAsync(int bookId, BookDto dto)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) throw new Exception("Book not found");

            ValidateBook(dto);

            book.Title = dto.Title;
            book.Genre = dto.Genre;
            book.Description = dto.Description;
            book.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) throw new Exception("Book not found");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            return await _context.Authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                FullName = a.FullName
            }).ToListAsync();
        }

        private void ValidateBook(BookDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new Exception("Title is required");

            if (!Enum.IsDefined(typeof(Genre), dto.Genre))
                throw new Exception("Invalid genre");

            if (dto.Description?.Length > 300)
                throw new Exception("Description must not exceed 300 characters");
        }
    }
}
