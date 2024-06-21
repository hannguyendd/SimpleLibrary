namespace SimpleLibrary.Domain.Entities;

public abstract class Entity<T> where T : notnull
{
    public T Id { get; protected set; } = default!;
    public DateTime CreatedAt { get; protected set; }
}