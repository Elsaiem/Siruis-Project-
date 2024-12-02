using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.RepositoryContract
{
    public interface IGenericRepository<TEntity> where TEntity: BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

       
        Task AddAsync(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        void Delete(TEntity entity);

        Task<int> CountEntity();

        void DeleteAll();


    }
}
