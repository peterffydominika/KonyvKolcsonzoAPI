using KonyvKolcsonzoAPI.Models;
using KonyvKolcsonzoAPI.Models.DTOs;
using KonyvKolcsonzoAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace KonyvKolcsonzoAPI.Services {
    public class BookService : IBookService {
        private readonly KonyvkolcsonzoContext _context;
        public ResponseDTO responseDto = new();

        public BookService(KonyvkolcsonzoContext context) {
            _context = context;
        }

        public async Task AddBookAsync(BookDTO dto) {
            var book = new Book {
                Title = dto.Title,
                Author = dto.Author
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<object> BorrowBookAsync(int bookId, int userId) {
            try
            {
                var book = await _context.Books.FindAsync(bookId);
                if (book == null)
                {
                    responseDto.Message = "A könyv nem található!";
                    responseDto.Result = null;
                    return responseDto;
                }
                if (book.UserId != null)
                {
                    responseDto.Message = "A könyv már ki van kölcsönözve!";
                    responseDto.Result = null;
                    return responseDto;
                }
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    responseDto.Message = "A felhasználó nem található!";
                    responseDto.Result = null;
                    return responseDto;
                }
                else
                {
                    book.UserId = userId;
                    await _context.SaveChangesAsync();
                    responseDto.Message = "A könyv sikeresen kölcsönözve!";
                    responseDto.Result = new GetBooksDTO {
                        Title = book.Title,
                        Author = book.Author,
                        Username = user.UserName
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hiba történt a könyv kölcsönzése során: " + ex.Message);
            }
            return responseDto;
        }

        public async Task<List<GetBooksDTO>> GetBorrowedBooksAsync() {
            return await _context.Books
                .Where(b => b.UserId != null)
                .Include(b => b.User)
                .Select(b => new GetBooksDTO {
                    Title = b.Title,
                    Author = b.Author,
                    Username = b.User.UserName
                })
                .ToListAsync();
        }

        Task<object> IBookService.BorrowBookAsync(int bookId, int userId) {
            throw new NotImplementedException();
        }
    }
}