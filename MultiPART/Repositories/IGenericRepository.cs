using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MultiPART.Models;
using MultiPART.UnitOfWork;

namespace MultiPART.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, ISoftDeletable
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetDeleted();
        IQueryable<TEntity> GetCurrent();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Undelete(TEntity entity);
        void Edit(TEntity entity);
    }

    public interface IGenericRepository<TEntity, in TKey> : IGenericRepository<TEntity>
        where TEntity : class ,ISoftDeletable
        where TKey : IEquatable<TKey>
    {
        TEntity Get(TKey key);
    }

    public interface IGenericRepository<TContext, TEntity, in TKey> : IGenericRepository<TEntity, TKey>
        where TContext : IDbContext
        where TEntity : class ,ISoftDeletable
        where TKey : IEquatable<TKey>
    {
        TContext Context { get; set; }
    }

    public interface IInsertOrUpdateRepository<TContext, TEntity, in TKey> : IGenericRepository<TContext, TEntity, TKey>
        where TContext : IDbContext
        where TEntity : class, ISoftDeletable
        where TKey : IEquatable<TKey>
    {
        void InsertOrUpdate(IEnumerable<TEntity> entity);
    }
}