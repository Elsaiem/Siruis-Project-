using Siruis_Project.Core.Entities;
using Siruis_Project.Core.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core
{
    public interface IUnitOfWork
    {
        Task<int>CompleteAsync();

        //Create Repository<T> Ans Returns

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity:BaseEntity;



    }
}
