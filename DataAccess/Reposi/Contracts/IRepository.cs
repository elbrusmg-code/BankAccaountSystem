using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Reposi.Contracts
{
    public interface IRepository<T>
    {
        List<T> GetAll(
     Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
     Expression<Func<T, bool>>? predicate = null);
        T? GetById(int id);
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
