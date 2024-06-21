using SimpleLibrary.Domain.Shared.Exceptions;

namespace SimpleLibrary.Domain.Entities;

public class BorrowedBook
{
    public Guid AccountId { get; private set; }
    public Guid BookId { get; private set; }

    public double PriceWhenBorrowed { get; private set; }
    public DateTime BorrowedAt { get; private set; }
    public DateTime ExpiredAt { get; private set; }
    public DateTime? ReturnedAt { get; private set; }

    public Account Account { get; private set; } = null!;
    public Book Book { get; private set; } = null!;

    private BorrowedBook() { }

    public BorrowedBook(Guid accountId, Book book, DateTime borrowedAt, DateTime expireAt)
    {
        AccountId = accountId;
        BookId = book.Id;
        PriceWhenBorrowed = book.Price;
        BorrowedAt = borrowedAt;
        ExpiredAt = expireAt;
    }

    public BorrowedBook(Guid accountId, Guid bookId, DateTime borrowedAt, DateTime expireAt, double priceWhenBorrowed)
    {
        AccountId = accountId;
        BookId = bookId;
        PriceWhenBorrowed = priceWhenBorrowed;
        BorrowedAt = borrowedAt;
        ExpiredAt = expireAt;
    }

    public void Return(DateTime returnedAt)
    {
        if (Book is null)
        {
            throw new BadRequestException("Book not found while returning");
        }

        ReturnedAt = returnedAt;
        Book.Return();
    }
}