using AutoMapper;
using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.BLL.Services.Contracts;
using LibraryManagementSystem.DAL.Enums;
using LibraryManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper, IAuthorService authorService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _authorService = authorService;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2)
        {
            var data = await _bookService.GetPaginatedBooksAsync(pageNumber, pageSize);
            var ViewModel = _mapper.Map<PaginatedBookViewModel>(data);
            return View(ViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var authors = await _authorService.ListAuthorsAsync();
            var model = new CreateBookViewModel();
            await PopulateDropdownsAsync(model);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View(model);
            }
            var dto = _mapper.Map<BookDto>(model);
            await _bookService.AddBookAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();
            var viewModel = _mapper.Map<CreateBookViewModel>(book);
            await  PopulateDropdownsAsync(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View(model);
            }
            var dto = _mapper.Map<BookDto>(model);
            await _bookService.UpdateBookAsync(model.Id, dto);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            await _bookService.DeleteBookAsync(id);
            TempData["AlertMessage"] = "Book Has Been Deleted!";

            return RedirectToAction(nameof(Index));
        }
        private async Task PopulateDropdownsAsync(CreateBookViewModel model)
        {
            model.GenreList = Enum.GetValues(typeof(Genre))
                .Cast<Genre>()
                .Select(g => new SelectListItem
                {
                    Value = g.ToString(),
                    Text = g.ToString()
                });

            var authors = await _authorService.ListAuthorsAsync();
            model.AuthorList = authors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FullName
            });
        }
    }
}
