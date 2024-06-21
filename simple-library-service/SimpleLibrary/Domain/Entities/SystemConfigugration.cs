namespace SimpleLibrary.Domain.Entities;

public class SystemConfiguration : Entity<Guid>
{
    public uint BookQuantity { get; private set; }
}