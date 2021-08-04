using ExchangeRatesBot.Domain.Interfaces;
using ExchangeRatesBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRatesBot.DB.Repositories
{
    public class RepositoryDb<T> : IBaseRepositoryDb<T> where T : Entity, new()
    {
        public Task<bool> Create(T user, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetCollection(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntity(T entity, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntityId(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T user, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
