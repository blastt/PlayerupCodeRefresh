using MarketplaceMVC.Data.EF;
using MarketplaceMVC.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(Expression<Func<T, bool>> where);

        T Get(Expression<Func<T, bool>> where);
        Task<T> GetAsync(Expression<Func<T, bool>> where);

        T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();

        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        T GetById(int id, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        #region Properties
        protected ApplicationContext db;
        protected readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ApplicationContext DbContext
        {
            get { return db ?? (db = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(Expression<Func<T, bool>> where)
        {
            dbSet.RemoveRange(dbSet.Where(where));
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.FirstOrDefault<T>(where);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return dbSet.FirstOrDefaultAsync<T>(where);
        }

        public T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.Where(where);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefault<T>();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.Where(where);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefaultAsync<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public Task<List<T>> GetAllAsync()
        {
            return dbSet.ToListAsync();
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var set = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                set = set.Include(include);
            }
            return set.ToList();
        }

        public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var set = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                set = set.Include(include);
            }
            return set.ToListAsync();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public Task<T> GetByIdAsync(int id)
        {
            return dbSet.FindAsync(id);
        }

        public T GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            var set = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                set = set.Include(include);
            }
            return set.SingleOrDefault(i => i.Id == id);
        }

        public Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var set = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                set = set.Include(include);
            }
            return set.SingleOrDefaultAsync(i => i.Id == id);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToListAsync();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var set = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                set = set.Include(include);
            }
            return set.ToList();
        }

        public Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var set = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                set = set.Include(include);
            }
            return set.ToListAsync();
        }

        #region Implementation




        #endregion

    }
}
