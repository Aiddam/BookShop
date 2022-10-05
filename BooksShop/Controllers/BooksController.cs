using BooksShop.Domain.Entity;
using BooksShop.Domain.ViewModel.Entity;
using BooksShop.Domain.ViewModel.Profile;
using BooksShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BooksShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        public static bool IsTrue;
        public BooksController(IBookService bookService, IUserService userService)
        {
            _bookService = bookService;
            _userService = userService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        
        {
            var response = await _bookService.GetAllBooks();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(string searchString)

        {
            var response = await _bookService.GetAllBooks();
            if (searchString !=null)
            {
                var books = response.Data.Where(x => x.Title.ToLower()!.Contains(searchString.ToLower()));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(books);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(response.Data);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BookViewModel bvm)
        {

            Book book = new();


            if (ModelState.IsValid)
            {
                if (bvm.Image != null)
                {
                    await _bookService.Create(bvm);
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(book);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null ||_bookService == null)
            {
                return NotFound();
            }
            var book = await _bookService.GetBook(id);
            await CheckedBook(book.Data);
          
            if (book == null)
            {
                return NotFound();
            }
            return View(book.Data);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null ||_bookService==null)
            {
                return NotFound();
            }
            var book = await _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book.Data);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_bookService == null)
            {
                return Problem("Entity set 'BookContext.Book'  is null.");
            }
            var book = await _bookService.GetBook(id);
            
                await _bookService.Delete(id);
            
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _bookService == null)
            {
                return NotFound();
            }
            var book = await _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Edit(BookViewModel book)
         {
           
            Book book1 = new(book);
            if (ModelState.IsValid)
            {
                try
                { 
                    await _bookService.Edit(book.Id, book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book1);
        }

       

        public async Task<IActionResult> AddFavorite (int? Id)
        {
            
            var book = await _bookService.GetBook(Id);
            var user = await _userService.GetUser(User.Identity.Name);
            var uvm = new UserViewModel(user.Data);
            uvm.FavoriteBook.Add(book.Data);
             await _userService.Edit(user.Data.Id,uvm);
            


            return RedirectToAction(nameof(Details),new {Id = Id});
        }
        
         public  async Task CheckedBook(Book book)
        {
            var user = await  _userService.GetUser(User.Identity.Name);
            if (user.Data.FavoriteBook !=null)
            {
                foreach (var item in user.Data.FavoriteBook)
                {
                    if (item.Id == book.Id)
                    {
                        IsTrue = true;
                        return;
                    }
                    else
                    {
                        IsTrue = false;
                    }
                }
            }
            IsTrue = false;
            return;
        }
    }
}
