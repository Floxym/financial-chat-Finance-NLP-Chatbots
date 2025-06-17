using Financial.Chat.Domain.Core.Interfaces.Repositories;
using Financial.Chat.Domain.Shared.Entity;
using Financial.Chat.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Financial.Chat.Infra.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly FinancialChatContext Db;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(FinancialChatContext financialChatContext)
        {
            Db = financialChatContext;
            DbSet = Db.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public IQueryable<T> GetAll() => DbSet;

        public IQueryable<T> GetByExpression(System.Linq.Expressions.Expression<Func<T, bool>> predicate) => DbSet.Where(predicate);

        public T GetById(Guid id) => DbSet.Find(id);

        public void Remove(T entity) 
        {
            DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
