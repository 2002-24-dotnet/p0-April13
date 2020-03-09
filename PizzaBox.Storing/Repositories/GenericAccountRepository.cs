using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Storing.Databases;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Storing.Repositories
{
  public class GenericAccountRepository<TEntity> : IGenericRepository<TEntity> where TEntity : AccountHolder
  {
    public ApplicationDbContext _db;
    public DbSet<TEntity> table;
     public GenericAccountRepository(ApplicationDbContext context)
    {
      _db = context;
      
      table = context.Set<TEntity>();
    }
/*
    public DbSet<TEntity> table
    {
      get
      {
        return _db.Set<TEntity>();
      }
    }
*/
    /*
    public GenericRepository()
    {
      _db = new ApplicationDbContext();
      table = _db.Set<TEntity>();
    }
    */

    public List<TEntity> GetAll()
    {
      return table.ToList();
    }

    public TEntity Get(long id)
    {
      // return table.Find(id);
      return table.SingleOrDefault(t => t.Id == id);
    }

    public bool Put(TEntity entity) // update
    {
      // table.Attach(entity);
      // _db.Entry(entity).State = EntityState.Modified;

      var p = Get(entity.Id);
      p = entity;
      return Save();
    }

    public bool Post(TEntity entity)  // insert
    {
      table.Add(entity);
      return Save();
    }

    public bool Delete(long id)
    {
      TEntity exisiting = table.Find(id);
      table.Remove(exisiting);
      return Save();
    }

    public bool Save()
    {
      return _db.SaveChanges() == 1;
    }
  }
}
