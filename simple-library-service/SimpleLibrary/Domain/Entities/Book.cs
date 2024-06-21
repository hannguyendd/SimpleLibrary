using SimpleLibrary.Domain.Shared.Exceptions;

namespace SimpleLibrary.Domain.Entities;

public class Book : Entity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int BorrowedQuantity { get; set; }

    public ICollection<BorrowedBook> BorrowedBooks { get; set; } = [];

    private Book() { }

    public Book(Guid id, string name, double price)
    {
        if (id == Guid.Empty)
        {
            throw new BadRequestException("Id is require");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BadRequestException("Name is require");
        }

        if (price < 0)
        {
            throw new BadRequestException("Price is not negative");
        }

        Id = id;
        Name = name;
        Price = price;
        BorrowedQuantity = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void Lend()
    {
        BorrowedQuantity++;
    }

    public void Return()
    {
        BorrowedQuantity--;
    }
}

