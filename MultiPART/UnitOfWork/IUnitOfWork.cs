using System;
using MultiPART.Models;
using MultiPART.Repositories;

namespace MultiPART.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity,int> GetRepository<TEntity>()
            where TEntity : class, ISoftDeletable;

        DataEntryRepository DataEntryRepository { get; set; }

        void Save();
    }
}