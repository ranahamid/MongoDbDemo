using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace RepositoryNoSQL
{
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : EntityBase
    {
        private IMongoCollection<TEntity> collection;
        private IMongoDatabase database;
        private MongoClient client;

        public MongoDbRepository()
        {
            string connectionString = ConfigurationManager.AppSettings.Get("MongoDbConnectionString");
            client = new MongoClient(connectionString); //  mongodb://127.0.0.1:27017
            
            database = client.GetDatabase(ConfigurationManager.AppSettings.Get("MongoDbDatabaseName")); //FXTF
            collection = database.GetCollection<TEntity>("entities");
        }
        public IEnumerable<TEntity> GetAll()
        {
            return collection.Find(new BsonDocument()).ToListAsync().Result;
        }
        public void Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            collection.InsertOneAsync(entity);
        }

        public TEntity FindById(Guid id)
        {
            return collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync().Result;
        }

        public Guid GetFirstId()
        {
            return collection.Find(new BsonDocument()).FirstOrDefault().Id;
           // return collection.Find(new BsonDocument()).ToEnumerable().FirstOrDefault().Id;            
        }

        public TEntity Update(TEntity entity)
        {
            if (entity.Id == null)
                Insert(entity);

            collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new UpdateOptions
            {
                IsUpsert = true
            });
            return entity;
        }

        public void Delete(Guid id)
        {
            collection.DeleteOneAsync(x => x.Id.Equals(id));
        }

    }
}
