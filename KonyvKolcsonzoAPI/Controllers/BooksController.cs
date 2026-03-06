using KonyvKolcsonzoAPI.Models.DTOs;
using KonyvKolcsonzoAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KonyvKolcsonzoAPI.Controllers {
    [ApiController]
    [Route("api/books")]
    [Authorize]
    public class BooksController : ControllerBase {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService) {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO dto) {
            try
            {
                await _bookService.AddBookAsync(dto);
                return Ok("Könyv sikeresen hozzáadva!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/borrow")]
        public async Task<IActionResult> BorrowBook(int id) {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                    return Unauthorized();

                int userId = int.Parse(userIdClaim);
                await _bookService.BorrowBookAsync(id, userId);
                return Ok("Könyv sikeresen kikölcsönözve!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBorrowedBooks")]
        public async Task<IActionResult> GetBorrowedBooks() {
            var books = await _bookService.GetBorrowedBooksAsync();
            return Ok(books);
        }
    }
}
