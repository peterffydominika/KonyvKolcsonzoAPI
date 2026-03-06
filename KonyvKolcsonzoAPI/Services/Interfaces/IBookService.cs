using KonyvKolcsonzoAPI.Models.DTOs;
namespace KonyvKolcsonzoAPI.Service.Interfaces {
    public interface IBookService {
        Task AddBookAsync(BookDTO bookdto);
        Task<object> BorrowBookAsync(int bookId, int userId);
        Task<List<GetBooksDTO>> GetBorrowedBooksAsync();
    }
}
