using Microsoft.EntityFrameworkCore;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models;
using TravelTracker.DataAccess.Context;

namespace TravelTracker.DataAccess.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly TravelTrackerDbContext _context;

        public RepositoryBase(TravelTrackerDbContext context)
        {
            _context = context;
        }

        public virtual async Task CreateAsync(T entity)
        {
            //try
            //{
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException ex)
            //{
            //    if (ex.InnerException is SqlException sqlEx)
            //    {

            //        Console.WriteLine("1111111111111111" + sqlEx.Message);
            //    }
            //}
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
