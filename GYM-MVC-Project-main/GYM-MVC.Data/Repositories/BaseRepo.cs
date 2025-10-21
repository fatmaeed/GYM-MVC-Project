using GYM.Domain.Entities;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GYM_MVC.Data.Repositories {

    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity {
        public GYMContext context;
        protected DbSet<T> dbSet;

        public BaseRepo(GYMContext _context) {
            context = _context;
            dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll() => dbSet.Where(entity => !entity.IsDeleted);

        public async Task<T> GetById(int id) => await dbSet.SingleOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

        public async Task Add(T entity) => await dbSet.AddAsync(entity);

        public async Task AddRange(IEnumerable<T> model) => await dbSet.AddRangeAsync(model);

        public void Update(T entity) {
            context.Entry(entity).State = EntityState.Modified;
            entity.UpdateDate = DateTime.Now;
        }

        public void Delete(int id) {
            var entity = GetById(id);
            entity.Result.IsDeleted = true;
            Update(entity.Result);
        }

        public void DeleteRange(IEnumerable<T> model) {
            for (int i = 0; i < model.Count(); i++) {
                model.ElementAt(i).IsDeleted = true;
                Update(model.ElementAt(i));
            }
        }

        public async Task<bool> Contains(T entity) => await dbSet.ContainsAsync(entity);

        public async Task<bool> Contains(Expression<Func<T, bool>> crieteria) => await dbSet.AnyAsync(crieteria);

        public async Task<bool> Any() => await dbSet.AnyAsync();

        public async Task<int> Count() => await dbSet.CountAsync();

        public async Task<int> Count(Expression<Func<T, bool>> crieteria) => await dbSet.CountAsync(crieteria);
    }
}