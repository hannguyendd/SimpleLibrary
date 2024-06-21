namespace SimpleLibrary.Application.Contracts.Dtos.Books;

public record CreateBorrowedBookDto(Guid UserId, Guid BookId);