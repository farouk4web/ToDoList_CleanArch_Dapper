using Nota.Application.Interfaces;
using Nota.Domain.Entities;
using System.Collections.Concurrent;
using System.Data;

namespace Nota.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        //private IDbTransaction _dbTransaction;
        private readonly ConcurrentDictionary<Type, object> _repositories;

        public UnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
            //_dbTransaction = _dbConnection.BeginTransaction();
            _repositories = new ConcurrentDictionary<Type, object>();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IGenericRepository<T>;
            }

            var repository = new GenericRepository<T>(_dbConnection);
            _repositories[typeof(T)] = repository;
            return repository;
        }

        public void Commit()
        {
            //if (_dbConnection.State == ConnectionState.Open)

            try
            {
                //_dbTransaction?.Commit();
            }
            catch
            {
                //_dbTransaction?.Rollback();
                throw;
            }
            finally
            {
                _dbConnection?.Close();
            }
        }

        public void Dispose()
        {
            //_dbTransaction?.Dispose();

            _dbConnection?.Dispose();
        }
    }
}