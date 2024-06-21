namespace SimpleLibrary.Application.Contracts.Dtos.Books;

public record BorrowedBookDto(BookDto Book, DateTime BorrowedAt, DateTime ExpiredAt, DateTime? ReturnedAt);