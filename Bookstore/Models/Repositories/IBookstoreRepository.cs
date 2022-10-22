using System.Collections.Generic;

namespace Bookstore.Models.Repositories
{
    public interface IBookstoreRepository <TEntity>
    {
        IList<TEntity> List();
        void Add(TEntity entity);   
        void Update(int id,TEntity entity);    
        void Delete(int id);
        TEntity Find(int id);

    }
}
