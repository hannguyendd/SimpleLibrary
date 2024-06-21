using SimpleLibrary.Application.Contracts.Dtos.Books;
using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Application.Contracts.Managers;

public interface IBookManager
{
    Task<BorrowedBook> BorrowBookAsync(CreateBorrowedBookDto borrowBook);
    Task<BorrowedBook> ReturnBookAsync(ReturnBookDto returnBook);
}