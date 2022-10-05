using BooksShop.Domain.Entity;
using BooksShop.Domain.Enum;
using BooksShop.Domain.ViewModel.Profile;
using BooksShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity.Infrastructure;

namespace BooksShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult> Index()
        {

            var response = await _userService.GetUsers();
                  
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public async Task<ActionResult> Index(string SearchString)
        {
            var response = await _userService.GetUsers();
            if (SearchString != null)
            {
                var users = response.Data.Where(x => x.Name.ToLower()!.Contains(SearchString.ToLower()));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(users);
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

            [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = await _userService.GetUser(model.Id);
            var _user = new UserViewModel
            {
                Name = user.Data.Name,
                FavoriteBook = user.Data.FavoriteBook,
                Role = model.Role,
                RoleEnum = model.Role.ToString(),
            };
            if (true)
            {
                try
                {
                    await _userService.Edit(user.Data.Id, _user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_userService == null)
            {
                return Problem("Entity set 'BookContext.Book'  is null.");
            }
            var book = await _userService.GetUser(id);

            await _userService.DeleteUser(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
