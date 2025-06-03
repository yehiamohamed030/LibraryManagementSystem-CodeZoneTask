using AutoMapper;
using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.Web.ViewModels;

namespace LibraryManagementSystem.Web.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthorDto, AuthorViewModel>().ReverseMap();

            CreateMap<BookDto, BookViewModel>().ReverseMap();
            CreateMap<BookDto, CreateBookViewModel>().ReverseMap();
            CreateMap<PaginatedBookDto, PaginatedBookViewModel>().ReverseMap();

            CreateMap<BookStatusDto, BorrowingBookViewModel>().ReverseMap();
        }
    }
}
