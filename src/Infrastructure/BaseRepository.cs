using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using BEComentarios.Domain;

namespace BEComentarios.Infrastructure
{
    public interface IRepository<T>
    {
        T? GetById(int id);
        //ValueTask<T?> GetById(int id);
        ValueTask<T?> GetById(long id);
        ValueTask<T?> GetById(string id);
        IReadOnlyList<T> GetAll();
        Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate);
        IAsyncEnumerable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> Add(T item);
        void Update(T item);
        void Delete(T item);
    }

    public abstract class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            DbSet = _context.Set<T>();
        }

        public DbSet<T> DbSet { get; set; }

        public async Task<bool> Add(T item)
        {
            var result = await DbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return BaseRepository<T>.IsSucces(result.Collections.Count());
        }

        public void Delete(T item)
        {
            DbSet.Remove(item);
            var _ = _context.SaveChanges();
            //TODO: Dá para retornar bool com isSucess
        }

        public IAsyncEnumerable<T> Where(Expression<Func<T, bool>> predicate) => DbSet.Where(predicate).AsAsyncEnumerable();

        //public async ValueTask<T?> GetById(int id) => await DbSet.FindAsync(id);

        public T? GetById(int id) => DbSet.Find(id);

        public async ValueTask<T?> GetById(long id) => await DbSet.FindAsync(id);

        public async ValueTask<T?> GetById(string id) => await DbSet.FindAsync(id);

        public IReadOnlyList<T> GetAll() => DbSet.ToList().AsReadOnly();

        public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate) => await DbSet.FirstOrDefaultAsync(predicate);

        public void Update(T item)
        {
            DbSet.Update(item);
            var _= _context.SaveChanges();
            //TODO: Dá para retornar bool com isSucess
        }

        private static bool IsSucces(int rowsAffected) => rowsAffected > 0;

    }
}