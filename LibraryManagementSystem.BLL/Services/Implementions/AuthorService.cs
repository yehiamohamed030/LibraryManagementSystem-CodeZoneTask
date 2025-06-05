using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.BLL.Services.Contracts;
using LibraryManagementSystem.DAL.Data;
using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services.Implementions
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AuthorDto>> ListAuthorsAsync()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.Email,
                    Website = a.Website,
                    Bio = a.Bio,
                    Books = a.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Genre = b.Genre
                    }).ToList()
                }).ToListAsync();
        }
        public async Task<PaginatedAuthorDto> GetPaginatedAuthorsAsync(int pageNumber, int pageSize)
        {
            var totalRecords = _context.Authors.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var authors = await _context.Authors
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.Email,
                    Website = a.Website,
                    Bio = a.Bio,
                    Books = a.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Genre = b.Genre
                    }).ToList()
                }).ToListAsync();
            var Dto = new PaginatedAuthorDto
            {
                Authors = authors,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                pageSize = pageSize
            };
            return Dto;
        }
        public async Task<AuthorDto> GetAuthorByIdAsync(int authorId)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == authorId);

            if (author == null) throw new Exception("Author not found");

            return new AuthorDto
            {
                Id = author.Id,
                FullName = author.FullName,
                Email = author.Email,
                Website = author.Website,
                Bio = author.Bio,
                Books = author.Books.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Genre = b.Genre
                }).ToList()
            };
        }
        public async Task AddAuthorAsync(AuthorDto author)
        {
            ValidateAuthor(author.FullName, author.Email, author.Bio);

            if (!await IsAuthorNameUniqueAsync(author.FullName))
                throw new Exception("Author name must be unique");

            if (!await IsEmailUniqueAsync(author.Email))
                throw new Exception("Email must be unique");

            var newAuthor = new Author
            {
                FullName = author.FullName.Trim(),
                Email = author.Email.Trim(),
                Website = author.Website,
                Bio = author.Bio
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(int authorId, AuthorDto author)
        {
            var existing = await _context.Authors.FindAsync(authorId);
            if (existing == null) throw new Exception("Author not found");

            ValidateAuthor(author.FullName, author.Email, author.Bio);

            if (!await IsAuthorNameUniqueAsync(author.FullName, authorId))
                throw new Exception("Author name must be unique");

            if (!await IsEmailUniqueAsync(author.Email, authorId))
                throw new Exception("Email must be unique");

            existing.FullName = author.FullName;
            existing.Email = author.Email;
            existing.Website = author.Website;
            existing.Bio = author.Bio;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null) throw new Exception("Author not found");

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsAuthorNameUniqueAsync(string fullName, int? excludeId = null)
        {
            return !await _context.Authors
                .AnyAsync(a => a.FullName == fullName && (!excludeId.HasValue || a.Id != excludeId.Value));
        }

        public async Task<bool> IsEmailUniqueAsync(string email, int? excludeId = null)
        {
            return !await _context.Authors
                .AnyAsync(a => a.Email == email && (!excludeId.HasValue || a.Id != excludeId.Value));
        }

        private void ValidateAuthor(string fullName, string email, string bio)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new Exception("Full name is required");

            var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4 || parts.Any(p => p.Length < 2))
                throw new Exception("Full name must consist of four names, each with at least 2 characters");

            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase))
                throw new Exception("Invalid email address");

            if (bio?.Length > 300)
                throw new Exception("Bio must not exceed 300 characters");
        }
    }
}
