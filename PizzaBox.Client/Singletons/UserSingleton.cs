using System;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Databases;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.Singletons
{
  public class UserSingleton
  {
    private GenericRepository<User> UserRepository;
    private long AccountId;
    private static ApplicationDbContext Context;

    public UserSingleton(ApplicationDbContext AppContext, long AccId)
    {
      UserRepository = new GenericRepository<User>(AppContext);
      Context = AppContext;
      AccountId = AccId;
    }

    public void Options()
    {
      Console.WriteLine("Menu\n----- \n 1: Order Pizza \n 2: Order History \n 3: Logout");

      bool Valid = false;
      bool logout = false;
      do
      {
        Console.Write("Enter 1, 2, or 3: ");
        string input = Console.ReadLine();
        input = input.Replace(" ", "");

        switch (input)
        {
          case "1":
            Valid = true;
            PickStore();
            break;
          case "2":
            Valid = true;
            OrderHistory();
            break;
          case "3":
            logout = true;
            break;
        }
        if (!Valid)
          Console.WriteLine("Invalid Input");
      } while (!Valid && !logout);
    }

    private void PickStore()
    {
      GenericRepository<Store> StoreRepository = new GenericRepository<Store>(Context);
      var StoreList = StoreRepository.GetAll();
      int count = 1;
      foreach (var s in StoreList)
      {
        Console.WriteLine(count + ": " + s.ToString());
        count++;
      }
      bool valid = false;
      do
      {
        Console.Write("Enter store number to select store: ");
        int input = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i < count; i++)
        {
          if (input.Equals(i))
          {
            valid = true;
            Console.WriteLine("Ordering from " + StoreList[i - 1].Name + "\n--------------");
            StartOrder(StoreList[i - 1].Id);
          }
        }
        if (!valid)
          Console.WriteLine("Invalid Number!");
      } while (!valid);

    }
    private void StartOrder(long StoreId)
    {
      OrderSingleton order = new OrderSingleton(Context, AccountId, StoreId);
      order.BuildPizza();
    }

    private void OrderHistory()
    {
      
      var OrderRepo = new OrderRepository(Context);
      var orders = OrderRepo.GetAll();
      foreach (var o in orders)
      {
        if (o.User.Equals(UserRepository.Get(AccountId)))
        {
          Console.WriteLine(o.date.ToString() + " " + o.Pizzas.Count + " Pizzas for " + o.Price);
        }
      }
    }

    private void LastOrder()
    {
      DateTime date = DateTime.Now;
    }
  }
}