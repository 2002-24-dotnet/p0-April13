using System;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Databases;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.Singletons
{
  public class StoreSingleton
  {
    private GenericRepository<Store> StoreRepository;
    private long AccountId;
    private static ApplicationDbContext Context;

    public StoreSingleton(ApplicationDbContext AppContext, long AccId)
    {
      StoreRepository = new GenericRepository<Store>(AppContext);
      Context = AppContext;
      AccountId = AccId;
    }

    public void Options()
    {
      Console.WriteLine("Menu\n----- \n 1: Order History \n 2: Logout");

      bool Valid = false;
      bool logout = false;
      do
      {
        Console.Write("Enter 1, or 2: ");
        string input = Console.ReadLine();
        input = input.Replace(" ", "");

        switch (input)
        {
            case "1":
            Valid = true;
            OrderHistory();
            break;
          case "2":
            logout = true;
            break;
            default:
            break;
        }
        if (!Valid)
          Console.WriteLine("Invalid Input");
      } while (!Valid && !logout);
    }
    private void OrderHistory()
    {
      var OrderRepo = new OrderRepository(Context);
      var orders = OrderRepo.GetAll();
      decimal total = 0;
      foreach (var o in orders)
      {
        if (o.Store.Id.Equals(AccountId))
        {
          Console.WriteLine(o.date.ToString() + " " + o.Pizzas.Count + " Pizzas for " + o.Price);
          total += o.Price;
        }
      }
      Console.WriteLine("Total Revenue: " + total);
    }
  }
}