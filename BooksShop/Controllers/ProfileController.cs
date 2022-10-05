using BooksShop.Domain.Entity;
using BooksShop.Domain.ViewModel.Profile;
using BooksShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;

namespace BooksShop.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        [Authorize]
        public async Task<IActionResult> Detail()
        {
            var userName = User.Identity.Name;
            var response = await _profileService.GetProfile(userName);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
             {
                return View(response.Data);
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Edit()
        {

            if ( _profileService == null)
            {
                return NotFound();
            }
            var book = await _profileService.GetProfile(User.Identity.Name);
            if (book == null)
            {
                return NotFound();
            }
            return View(book.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(ProfileViewModel pfm)
        {
            Profile profile = new Profile(pfm);
            if (true)
            {
                try
                {
                    await _profileService.Save(pfm);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Detail));
            }
        }
    }
}
