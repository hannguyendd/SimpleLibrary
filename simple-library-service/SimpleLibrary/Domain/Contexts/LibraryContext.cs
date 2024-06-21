using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Shared.Utilities;

namespace SimpleLibrary.Domain.Contexts;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    public DbSet<SystemConfiguration> SystemConfigurations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        SeedAccounts(modelBuilder);

        SeedBooks(modelBuilder);
    }

    private static void SeedAccounts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasData(CreateAccount("reader", "Reader", "ThisIsAStrongPassword", new Guid("8b2db0e6-16bb-40b3-83ec-37ae496f82c0")));
    }

    private static void SeedBooks(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book(new Guid("68ce8dfa-58ef-4a18-a4fd-83104459e6a0"), "Clean Code", 5),
            new Book(new Guid("a5f0a9e3-3b60-4d1e-92c6-2a34a1a7e51d"), "The Pragmatic Programmer", 3),
            new Book(new Guid("0f21a0b1-4f1d-4e31-90f1-5b09bbcdab61"), "Refactoring", 7),
            new Book(new Guid("e9b5e362-4b19-4c87-9b2f-f1d740bde40b"), "Design Patterns", 4),
            new Book(new Guid("d2c6b66f-2e65-4d87-8d61-24a0d65e50c3"), "Code Complete", 6),
            new Book(new Guid("ef36b5cc-6c32-47ed-841e-1a86d1bfa596"), "The Mythical Man-Month", 2),
            new Book(new Guid("3c6e2a94-1f88-4d27-91f5-3f4c093349a6"), "Head First Design Patterns", 8),
            new Book(new Guid("73dfbfa4-7b45-4f1b-b51e-cd2b53cfa3b0"), "Continuous Delivery", 5),
            new Book(new Guid("e5df2b68-4b38-45e1-a57e-09395b5975d2"), "Clean Architecture", 4)
            );

        modelBuilder.Entity<BorrowedBook>().HasData(
            // new BorrowedBook(
            //     new Guid("8b2db0e6-16bb-40b3-83ec-37ae496f82c0"),
            //     new Book(new Guid("8a7fdf33-2a49-4bde-9f28-d9a63e4f41a1"), "Domain-Driven Design", 6),
            //     new DateTime(2024, 6, 5, 0, 0, 0, DateTimeKind.Local),
            //     new DateTime(2024, 6, 19, 0, 0, 0, DateTimeKind.Local)
            // ),
            new BorrowedBook(
                new Guid("8b2db0e6-16bb-40b3-83ec-37ae496f82c0"),
                new Guid("e5df2b68-4b38-45e1-a57e-09395b5975d2"),
                new DateTime(2024, 6, 5, 0, 0, 0, DateTimeKind.Local),
                new DateTime(2024, 6, 19, 0, 0, 0, DateTimeKind.Local),
                5
            ));
    }

    private static void SeedBorrowedBooks(ModelBuilder modelBuilder)
    {

    }

    private static Account CreateAccount(string username, string name, string password, Guid? id = null)
    {
        HashPasswordUtility.HashPassword(password, out byte[] hashPass, out byte[] salt);

        return new Account(id ?? Guid.NewGuid(), username, name, hashPass, salt);
    }
}