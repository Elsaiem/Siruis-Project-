using Siruis_Project.Core.Entities;
using Siruis_Project.Core.RepositoryContract;
using Siruis_Project.Core;
using Siruis_Project.Repository.Data.Contexts;
using Siruis_Project.Repository.Repositories;
using System.Collections;

public class UnitOfWork : IUnitOfWork
{
    private readonly SiruisDbContext _Context;
    private Hashtable _repository;

    public UnitOfWork(SiruisDbContext storeDbContext)
    {
        _Context = storeDbContext;
        _repository = new Hashtable();
    }

    public async Task<int> CompleteAsync() => await _Context.SaveChangesAsync();

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity);

        if (!_repository.ContainsKey(type))
        {
            var repository = new GenericRepository<TEntity>(_Context);
            _repository.Add(type, repository); // Use `type` as key
        }

        return _repository[type] as IGenericRepository<TEntity>;
    }
}
