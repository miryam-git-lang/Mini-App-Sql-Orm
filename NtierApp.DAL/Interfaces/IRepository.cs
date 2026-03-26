using Microsoft.EntityFrameworkCore.ChangeTracking;
using NtierApp.Core.Models;
using NtierApp.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.DAL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
		IQueryable<T> GetAll();
		IQueryable<T> GetAll(bool isTracking = false,
		Expression<Func<T,bool>>? filter = null,
		int page = 1, 
		int take = 2,
		params string[] includes);
		Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
		void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
		Task<bool> IsExistAsync(Expression<Func<T, bool>>? filter = null);
    }
}
