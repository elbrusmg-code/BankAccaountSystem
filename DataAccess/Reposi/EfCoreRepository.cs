using DataAccess.Data;
using DataAccess.Reposi.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Reposi;

public class EfCoreRepository<T> : IRepository<T> where T : class
{
    protected readonly BankContext AppDbContext;

    public EfCoreRepository(BankContext context)
    {
        AppDbContext = context;
    }

    public List<T> GetAll(
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = AppDbContext.Set<T>().AsNoTracking();

        if (include != null)
            query = include(query);

        if (predicate != null)
            query = query.Where(predicate);

        return query.ToList();
    }

    public T? GetById(int id)
        => AppDbContext.Set<T>().Find(id);

    public void Add(T entity)
    {
        AppDbContext.Set<T>().Add(entity);
        AppDbContext.SaveChanges();
    }

    public void Update(int id, T entity)
    {
        AppDbContext.Set<T>().Update(entity);
        AppDbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            AppDbContext.Set<T>().Remove(entity);
            AppDbContext.SaveChanges();
        }
    }
}