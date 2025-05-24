using HospitalManagementSystem.Data;
using HospitalManagementSystem.Entity.Common;
using HospitalManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;


        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null || !entity.IsDeleted)
            {
                return false;
            }
            entity.IsDeleted = true; // Soft delete
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            var entities = (await _dbSet.ToListAsync()).Where(x => !x.IsDeleted);
            return entities.AsQueryable();


        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null || !entity.IsDeleted)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            return entity;

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }
            entity.UpdatedAt = DateTime.UtcNow; // Update timestamp
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
