using BooksShop.Domain.Entity;
using BooksShop.Domain.ViewModel.Entity;
using BooksShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksShop.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IBookService bookService, IUserService userService)
        {
            _messageService = messageService;
            _bookService = bookService;
            _userService = userService;
        }


        public async Task<IActionResult> CreateMessage(int bookId, string message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var book = await _bookService.GetBook(bookId);
                    var user = await _userService.GetUser(User.Identity.Name);
                    var UserMessage = new MessageViewModel()
                    {
                        Report = message,
                        BookId = bookId,
                        Book = book.Data,
                        User = user.Data,
                        UserId = user.Data.Id,
                        DateCreate = DateTime.Now
                    };
                    await _messageService.Create(UserMessage);
                    return Redirect("/Books/Details/" + book.Data.Id);

                }
                catch (Exception)
                {
                    throw;
                }

               
            }
            return NotFound();
           
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var message = await _messageService.GetMessage(id);
            try
            {
                await _messageService.Delete(id);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            
            return Redirect("/Books/Details/"+ message.Data.BookId);
        }
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _messageService == null)
            {
                return NotFound();
            }
            var message = await _messageService.GetMessage(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MessageViewModel message)
        {
            var message1 = await _messageService.GetMessage(message.Id);
            message1.Data.Report = message.Report;
            message1.Data.DateChange = DateTime.Now;
            message.Changed(message1.Data);

                try
                {
                    await _messageService.Edit(message1.Data.Id,message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            return Redirect("/Books/Details/" + message1.Data.BookId);

            return View(message1.Data);
        }
    }
}
