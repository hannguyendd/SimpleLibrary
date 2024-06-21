using Microsoft.EntityFrameworkCore;

namespace SimpleLibrary.Infrastructure.Repositories;

public abstract class GenericRepository<T>(DbContext context) where T : class
{
    public IQueryable<T> GetQueryable()
    {
        return context.Set<T>().AsQueryable();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToArrayAsync();
    }

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }
}