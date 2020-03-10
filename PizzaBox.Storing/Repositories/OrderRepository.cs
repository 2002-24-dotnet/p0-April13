using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Databases;

namespace PizzaBox.Storing.Repositories
{
  public class OrderRepository
  {
    private static ApplicationDbContext _db;

    public OrderRepository(ApplicationDbContext context)
    {
      _db = context;
    }

    public List<Order> GetAll()
    {
      return _db.Order.Include(o => o.User).Include(o => o.Store).ToList();
    }

    public Order Get(long id)
    {
      return _db.Order.SingleOrDefault(o => o.OrderId == id);
    }

    public bool Post(Order order)
    {
      _db.Order.Add(order);
      return _db.SaveChanges() == 1;
    }

    public bool Put(Order order)
    {
      Order o = Get(order.OrderId);

      o = order;
      return _db.SaveChanges() == 1;
    }
  }
}
