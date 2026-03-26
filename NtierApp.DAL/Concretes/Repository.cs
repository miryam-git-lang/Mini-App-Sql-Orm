using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NtierApp.Core.Models;
using NtierApp.Core.Models.Common;
using NtierApp.DAL.Context;
using NtierApp.DAL.Interfaces;

namespace NtierApp.DAL.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context;
        private DbSet<T> Table => context.Set<T>();
        public Repository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);    
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return Table.AsQueryable();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll(bool isTracking = false, Expression<Func<T, bool>>? filter = null, int page = 1, int take = 2, params string[] includes)
        {
            var query = Table.AsQueryable();
            if(!isTracking) 
                query = query.AsNoTracking();
            if(filter != null)
                query = query.Where(filter);

            foreach (var include in includes)
                query = query.Include(include);
            query = query.Skip((page - 1) * take).Take(take);

            return query;

        }

        public Task<bool> IsExistAsync(Expression<Func<T, bool>>? filter = null)
        {
            return Table.AnyAsync(filter);
        }
    }
}
