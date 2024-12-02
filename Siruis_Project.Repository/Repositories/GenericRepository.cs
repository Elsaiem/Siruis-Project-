using Microsoft.EntityFrameworkCore;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.RepositoryContract;
using Siruis_Project.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Repository.Repositories
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public SiruisDbContext _Context { get; }

        public GenericRepository(SiruisDbContext storeDbContext)
        {
            _Context = storeDbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           
            return await _Context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetAsync(int id)
        {
            var entity = await _Context.Set<TEntity>().FindAsync(id);
            if (entity == null) return null;
            return entity;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null) return null;

            _Context.Set<TEntity>().Update(entity);
            var changes = await _Context.SaveChangesAsync(); // Save changes to persist the update

            return entity; // Return true if changes were made, otherwise false
        }


        public void Delete(TEntity entity)
        {
            _Context.Set<TEntity>().Remove(entity);
        }

        public void DeleteAll()
        {
            var dbSet = _Context.Set<TEntity>();
            _Context.RemoveRange(dbSet); // Deletes all entities in the DbSet
           
        }
         
        public async Task<int> CountEntity()
        {

            return await  _Context.Set<TEntity>().CountAsync();
        }
      

    }
}
