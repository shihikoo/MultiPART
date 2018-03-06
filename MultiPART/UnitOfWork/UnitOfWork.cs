using System;
using System.Collections.Generic;
using System.Linq;
using MultiPART.Models;
using MultiPART.Repositories;

namespace MultiPART.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : IDbContext, new()
    {
        private readonly TContext _ctx;
        private readonly Dictionary<Type, object> _repositories;
        private DataEntryRepository _dataEntryRepository ;
        private bool _disposed;
        

        public UnitOfWork()
        {
            _ctx = new TContext();
            _repositories = new Dictionary<Type, object>();
            _disposed = false;
        }

        public IGenericRepository<TEntity, int> GetRepository<TEntity>()
            where TEntity : class, ISoftDeletable
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IGenericRepository<TEntity, int>;

            var repository = new GenericRepository<TContext, TEntity, int>(_ctx);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public DataEntryRepository DataEntryRepository
        {
            get { return _dataEntryRepository ?? (_dataEntryRepository = new DataEntryRepository(_ctx)); }
            set { _dataEntryRepository = value; }
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }

                _disposed = true;
            }
        }

    }
}