using System.Collections.Generic;

namespace PizzaBox.Domain.Interfaces
{
  public interface IGenericRepository<TEntity> where TEntity : class, IEntity
  {
    List<TEntity> GetAll();        // get all
    TEntity Get(long Id);    // get/find by Id
    bool Post(TEntity entity);  // add/insert
    bool Put(TEntity entity);  // update

    bool Delete(long id);  // delete
    bool Save();
  }
}