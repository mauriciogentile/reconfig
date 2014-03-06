using System.Collections.Generic;
using System.Linq;
using Reconfig.Domain.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Reconfig.Domain.Repositories;
using Reconfig.Storage.Mongo.Extensions;

namespace Reconfig.Storage.Mongo
{
    public class DomainRepository<TRoot> : MongoRepository, IDomainRepository<TRoot> where TRoot : AggregateRoot
    {
        private readonly MongoCollection<TRoot> _collection;

        public DomainRepository(string connectionString)
            : base(connectionString)
        {
            _collection = Db.GetCollection<TRoot>(typeof(TRoot).Name);
        }

        public IQueryable<TRoot> Find()
        {
            return _collection.AsQueryable();
        }

        public IEnumerable<TRoot> GetAll()
        {
            return _collection.FindAll();
        }

        public TRoot Get(string id)
        {
            return _collection.FindById(id);
        }

        public void Save(TRoot entity)
        {
            _collection.Save(entity);
        }

        public void Delete(string id)
        {
            _collection.Remove(id);
        }
    }
}