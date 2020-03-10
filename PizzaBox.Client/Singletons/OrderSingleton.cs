using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Databases;
using PizzaBox.Storing.Repositories;

// Builds a Pizza
namespace PizzaBox.Client.Singletons
{
  public class OrderSingleton
  {
    private GenericRepository<Crust> CrustRepository;
    private GenericRepository<Size> SizeRepository;
    private GenericRepository<Topping> ToppingRepository;
    // private GenericRepository<PizzaTopping> PizzaToppingRepository;

    private static PizzeriaSingleton _ps;

    private static OrderSingleton _os;
    private static ApplicationDbContext Context;

    private long UserId;
    private long StoreId;
    private Order currentOrder;

    public OrderSingleton(ApplicationDbContext AppContext, long user, long store)
    {

      CrustRepository = new GenericRepository<Crust>(AppContext);
      SizeRepository = new GenericRepository<Size>(AppContext);
      ToppingRepository = new GenericRepository<Topping>(AppContext);

      _ps = new PizzeriaSingleton(AppContext);
      Context = AppContext;

      UserId = user;
      StoreId = store;
      currentOrder = new Order();
      // _os = new OrderSingleton(AppContext);
    }


    public static OrderSingleton Instance
    {
      get
      {
        return _os;
      }
    }
    public List<Crust> Get()
    {
      return CrustRepository.GetAll();
    }

    public void BuildPizza()
    {
      // Console.WriteLine("User: " + UserId + " Store: " + StoreId);
      Console.WriteLine("Build A Pizza\n--------------");

      // print sizes and get size from user
      Console.WriteLine("Select Size\n--------------");
      var sizes = SizeRepository.GetAll();
      int count = 1;
      foreach (var s in sizes)
      {
        Console.WriteLine(count + ": " + s.Name);
        count++;
      }

      int input = MatchInput(count);
      Size size = sizes[input - 1];

      // get crust from user
      Console.WriteLine("Select Crust\n--------------");
      var crusts = CrustRepository.GetAll();
      count = 1;
      foreach (var c in crusts)
      {
        Console.WriteLine(count + ": " + c.Name);
        count++;
      }
      input = MatchInput(count);
      Crust crust = crusts[input - 1];

      // get toppings
      Console.WriteLine("Select Toppings (up to 5)\n-------------------------");
      var toppings = ToppingRepository.GetAll();
      int toppingCount = 0;
      bool done = false;
      List<Topping> toppingList = new List<Topping>();
      // print list toppings
      count = 1;
      foreach (var t in toppings)
      {
        Console.WriteLine(count + ": " + t.Name);
        count++;
      }
      Console.WriteLine(count + ": Done Adding Toppings");
      do
      {
        input = MatchInput(count + 1);

        if (input.Equals(count))
          done = true;
        else
        {
          toppingList.Add(toppings[input - 1]);
          toppingCount++;
        }
      } while (!done && toppingCount < 5);
      string toppingString = "";
      foreach (var t in toppingList)
      {
        toppingString += " " + t.Name + ",";
      }
      toppingString = toppingString.Remove(toppingString.LastIndexOf(","));

      string PizzaName = size.Name + " " + crust.Name + " with" + toppingString;
      // Confirm Pizza 
      Console.WriteLine("You are ordering a " + PizzaName);
      Console.WriteLine("1: Confirm Pizza");
      Console.WriteLine("2: Redo Pizza");
      Console.WriteLine("3: Exit");
      input = MatchInput(4);
      if (input.Equals(1))
      {
        // set user and store id to order & post order if not yet done
        if (currentOrder.User == null)
        {
          var UserRepo = new GenericRepository<User>(Context);
          var StoreRepo = new GenericRepository<Store>(Context);
          var OrderRepo = new OrderRepository(Context);
          // set order id
          currentOrder.date = DateTime.Now;
          OrderRepo.Post(currentOrder);
          currentOrder = OrderRepo.GetAll().Last();
          UserRepo.Get(UserId).Orders = new List<Order> { currentOrder };
          StoreRepo.Get(StoreId).Orders = new List<Order> { currentOrder };
          // set user and store id in order
          currentOrder.User = UserRepo.Get(UserId);
          currentOrder.Store = StoreRepo.Get(StoreId);
          OrderRepo.Put(currentOrder);
        }

        _ps.Post(PizzaName, crust, size, toppingList, currentOrder);
        Console.WriteLine("1: Order Another");
        Console.WriteLine("2: Finished");
        input = MatchInput(3);
        if (input.Equals(1))
          BuildPizza();
        else
        {
          Console.WriteLine("Thank you for ordering with us!");
          var back = new UserSingleton(Context, UserId);
          back.Options();
        }
      }
      else if (input.Equals(2))
      {
        BuildPizza();
      }
      else
      {
        Console.WriteLine("No pizza for you../");
        var back = new UserSingleton(Context, UserId);
        back.Options();
      }

    }

    private int MatchInput(int count)
    {
      bool valid = false;
      int input;
      do
      {
        Console.Write("Enter number to select: ");
        input = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= count; i++)
        {
          if (input.Equals(i))
          {
            valid = true;
          }
        }
        if (!valid)
          Console.WriteLine("Invalid Number!");

      } while (!valid);

      return input;
    }

  }
}