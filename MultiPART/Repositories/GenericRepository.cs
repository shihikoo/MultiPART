using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MultiPART.Models;
using MultiPART.UnitOfWork;

namespace MultiPART.Repositories
{
    public class GenericRepository<TContext, TEntity,TKey> :
        IGenericRepository<TEntity,TKey>
        where TEntity : class, ISoftDeletable
        where TKey: IEquatable<TKey>
        where TContext: IDbContext

    {
        private readonly TContext _entities;

        public GenericRepository(TContext context)
        {
            _entities = context;
        }


        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>();
            return query;
        }

        public IQueryable<TEntity> GetDeleted()
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>()
                .Where(e => e.Status == "Deleted");
            return query;
        }

        public IQueryable<TEntity> GetCurrent()
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>()
                 .Where(e => e.Status == "Current");
            return query;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>().Where(predicate);
            return query;
        }

        public void Add(TEntity entity)
        {
            _entities.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.Status = "Deleted";
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public void Undelete(TEntity entity)
        {
            entity.Status = "Current";
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public void Edit(TEntity entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public TEntity Get(TKey key)
        {
            return _entities.Set<TEntity>().Find(key);
        }

        public void InsertOrUpdate(TEntity entity)
        {
            var key = (int) entity.GetType().GetProperties().First(prop => Attribute.IsDefined(prop, typeof (KeyAttribute))).GetValue(entity);
            if (key == 0) {Add(entity);}
            else
            {
                Edit(entity);
            }
        }
    }

}