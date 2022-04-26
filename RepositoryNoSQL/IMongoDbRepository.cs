using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryNoSQL
{
    public interface IMongoDbRepository<TEntity> where TEntity : EntityBase
    {
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        TEntity FindById(Guid id);
        TEntity Update(TEntity entity);
        void Delete(Guid id);
        Guid GetFirstId();
    }
}
