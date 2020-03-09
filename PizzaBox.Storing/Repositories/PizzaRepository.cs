using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Databases;

namespace PizzaBox.Storing.Repositories
{
  public class PizzaRepository
  {
    private static ApplicationDbContext _db;

    public PizzaRepository(ApplicationDbContext context)
    {
      _db = context;
    }

    public List<Pizza> GetAll()
    {
      return _db.Pizza.Include(p => p.Crust).Include(p => p.Size).Include(p => p.PizzaToppings).ToList();
    }

    public Pizza Get(long id)
    {
      return _db.Pizza.SingleOrDefault(p => p.Id == id);
    }

    public bool Post(Pizza pizza)
    {
      _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Pizza ON");
      _db.Pizza.Add(pizza);
      return _db.SaveChanges() == 1;
    }

    public bool Put(Pizza pizza)
    {
      Pizza p = Get(pizza.Id);

      p = pizza;
      return _db.SaveChanges() == 1;
    }
  }
}
