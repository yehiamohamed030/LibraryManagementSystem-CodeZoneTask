using LibraryManagementSystem.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services.Contracts
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> ListAuthorsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(int authorId);
        Task AddAuthorAsync(AuthorDto author);
        Task UpdateAuthorAsync(int authorId, AuthorDto author);
        Task DeleteAuthorAsync(int authorId);
    }
}
