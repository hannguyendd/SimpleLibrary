using SimpleLibrary.Domain.Shared.Constants;

namespace SimpleLibrary.Domain.Entities;

public class Account : Entity<Guid>
{
    public string Username { get; private set; }
    public string Name { get; private set; }
    public byte[] HashPassword { get; private set; }
    public byte[] Salt { get; private set; }
    public double CreditAmount { get; private set; }

    public ICollection<BorrowedBook> BorrowedBooks { get; set; } = [];

    public Account(Guid id, string username, string name, byte[] hashPassword, byte[] salt)
    {
        Id = id;
        Username = username;
        Name = name;
        HashPassword = hashPassword;
        Salt = salt;
        CreditAmount = AppDefaultValueConstant.DefaultUserCreditAmount;
    }

    public void Spend(double amount)
    {
        CreditAmount -= amount;
    }
}