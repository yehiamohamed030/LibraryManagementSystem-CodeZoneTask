using AutoMapper;
using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.Web.ViewModels;

namespace LibraryManagementSystem.Web.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDto, AuthorViewModel>().ReverseMap();
            CreateMap<BookDto, BookViewModel>().ReverseMap();
        }
    }
}
