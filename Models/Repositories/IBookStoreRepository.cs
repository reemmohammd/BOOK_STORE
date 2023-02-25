using System.Collections.Generic;

namespace BOOK_STORE.Models.Repositories
{
    public interface IBookStoreRepository<TEntity>// TEntity to be generic works as any class or type (book,Author,..) 
                      // to implement class like Book.. except of type TEntity we write Book .
    {
        

        IList<TEntity> List();
        TEntity find(int id); // when we search we return a value so we used TEntity
        void add (TEntity entity);  // we dont have to return a value so we use void
        void update (int id,TEntity entity);   
        void Delete (int id);   
    }
}
