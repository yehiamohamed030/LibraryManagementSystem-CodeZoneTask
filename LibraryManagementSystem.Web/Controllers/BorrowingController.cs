using AutoMapper;
using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.BLL.Services.Contracts;
using LibraryManagementSystem.BLL.Services.Implementions;
using LibraryManagementSystem.DAL.Enums;
using LibraryManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Web.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IMapper _mapper;
        public BorrowingController(IBorrowingService borrowingService, IMapper mapper)
        {
            _borrowingService = borrowingService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(Status? status, DateTime? borrowDate, DateTime? returnDate)
        {
            var filteredBooks = await _borrowingService.FilterBooksAsync(status, borrowDate, returnDate);
            var viewModel = _mapper.Map<IEnumerable<BorrowingBookViewModel>>(filteredBooks);
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var books = await _borrowingService.ListBooksWithStatusAsync();
            var model = new CreateBorrowBookViewModel();
            model.Books = books.Select(a => new SelectListItem
            {
                Value = a.BookId.ToString(),
                Text = a.Title
            });
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBorrowBookViewModel model)
        {
            await _borrowingService.BorrowBookAsync(model.bookId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CheckAvailability(int bookId)
        {
            var isAvailable = await _borrowingService.IsBookAvailableAsync(bookId);
            return Json(new { available = isAvailable});
        }
        [HttpGet]
        public async Task<IActionResult> Return()
        {
            var books = await _borrowingService.FilterBooksAsync(Status.Borrowed);
            var model = new CreateBorrowBookViewModel();
            model.Books = books.Select(a => new SelectListItem
            {
                Value = a.BookId.ToString(),
                Text = a.Title
            });
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Return(CreateBorrowBookViewModel model)
        {
            await _borrowingService.ReturnBookAsync(model.bookId);
            return RedirectToAction(nameof(Index));
        }
    }
}
