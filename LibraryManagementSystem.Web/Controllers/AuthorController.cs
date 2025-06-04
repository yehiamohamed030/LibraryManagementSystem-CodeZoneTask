using AutoMapper;
using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.BLL.Services.Contracts;
using LibraryManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Web.Controllers
{
    public class AuthorController : Controller
    {

        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _authorService.ListAuthorsAsync();
            var dataVM = _mapper.Map<IEnumerable<AuthorViewModel>>(data);
            return View(dataVM);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var dto = _mapper.Map<AuthorDto>(model);
            await _authorService.AddAuthorAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();
            var viewModel = _mapper.Map<AuthorViewModel>(author);
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var dto = _mapper.Map<AuthorDto>(model);
            await _authorService.UpdateAuthorAsync(model.Id,dto);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {            
            await _authorService.DeleteAuthorAsync(id);
            TempData["AlertMessage"] = "Author Has Been Deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
