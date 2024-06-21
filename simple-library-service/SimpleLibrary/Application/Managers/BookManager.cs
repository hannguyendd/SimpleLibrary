using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Application.Contracts.Dtos.Books;
using SimpleLibrary.Application.Contracts.Managers;
using SimpleLibrary.Application.Contracts.Services;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Repositories;
using SimpleLibrary.Domain.Shared.Constants;
using SimpleLibrary.Domain.Shared.Exceptions;

namespace SimpleLibrary.Application.Managers;

public class BookManager : IBookManager
{
    private readonly IBookRepository _bookRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IBorrowedBookRepository _borrowedBookRepository;
    private readonly ISystemConfigurationRepository _systemConfigurationRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public BookManager(IBookRepository bookRepository, IAccountRepository accountRepository, ISystemConfigurationRepository systemConfigurationRepository, IBorrowedBookRepository borrowedBookRepository, IDateTimeProvider dateTimeProvider)
    {
        _bookRepository = bookRepository;
        _accountRepository = accountRepository;
        _systemConfigurationRepository = systemConfigurationRepository;
        _borrowedBookRepository = borrowedBookRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<BorrowedBook> BorrowBookAsync(CreateBorrowedBookDto borrowBook)
    {
        var account = await GetAccountAsync(borrowBook.UserId);
        if (account.BorrowedBooks.Count(x => x.ReturnedAt == null) >= AppDefaultValueConstant.MaxBorrowedBook)
        {
            throw new BadRequestException("You have exceeded the maximum number of books that can be borrowed.");
        }

        var book = await GetBookAsync(borrowBook.BookId);
        if (book.Price > account.CreditAmount)
        {
            throw new BadRequestException("User's current credit balance is insufficient.");
        }

        var configuration = await _systemConfigurationRepository.GetOneAsync();
        var bookQuantity = configuration?.BookQuantity ?? AppDefaultValueConstant.DefaultBookQuantity;

        if (book.BorrowedQuantity >= bookQuantity)
        {
            throw new BadRequestException("The quantity of the book is insufficient");
        }

        book.Lend();
        var now = _dateTimeProvider.Now;
        var newBorrowedBook = new BorrowedBook(
            account.Id,
            book,
            now,
            now.AddDays(AppDefaultValueConstant.BookBorrowingDurationInDay));

        _borrowedBookRepository.Add(newBorrowedBook);

        return newBorrowedBook;
    }

    public async Task<BorrowedBook> ReturnBookAsync(ReturnBookDto returnBook)
    {
        var borrowedBook = await _borrowedBookRepository.GetQueryable()
            .Include(x => x.Book)
            .Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.AccountId == returnBook.UserId && x.BookId == returnBook.BookId && x.ReturnedAt == null)
            ?? throw new NotFoundException("User does not borrow this book");
        var returnedAt = _dateTimeProvider.Now;
        var daysForDelay = (returnedAt - borrowedBook.BorrowedAt).Days;

        borrowedBook.Return(returnedAt);
        if (daysForDelay > 0)
        {
            var penaltyRatio = Math.Min(1, daysForDelay * AppDefaultValueConstant.PenaltyRatioPerDayForDelay);
            borrowedBook.Account.Spend(borrowedBook.PriceWhenBorrowed * penaltyRatio);
        }

        return borrowedBook;
    }

    private async Task<Account> GetAccountAsync(Guid accountId)
    {
        var accountQueryable = _accountRepository.GetQueryable();
        return await accountQueryable.AsNoTracking().Include(x => x.BorrowedBooks).FirstOrDefaultAsync(x => x.Id == accountId)
            ?? throw new NotFoundException("User not found");
    }

    private async Task<Book> GetBookAsync(Guid bookId)
    {
        var bookQueryable = _bookRepository.GetQueryable();
        return await bookQueryable.FirstOrDefaultAsync(x => x.Id == bookId) ??
            throw new NotFoundException("Book not found");
    }
}