namespace LibraryManagementSystem.Web.ViewModels
{
    public class PaginatedAuthorViewModel
    {
        public IEnumerable<AuthorViewModel> Authors { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int pageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
