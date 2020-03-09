using System;
using PizzaBox.Client.Singletons;
using PizzaBox.Storing.Databases;

namespace PizzaBox.Client
{
  class Program
  {
    private static PizzeriaSingleton _ps;
    private static OrderSingleton _os;
    private static ApplicationDbContext AppContext = new ApplicationDbContext();
    private static SignUpSingleton _sus;

    static void Main(string[] args)
    {
      DatabaseInitializer Database = new DatabaseInitializer();
      Console.WriteLine("Connected: " + Database.InitializeDatabase(AppContext));

      _ps = new PizzeriaSingleton(AppContext);
      _os = new OrderSingleton(AppContext);

      _os.OrderedPizzas();
      GetAllPizzas();
      
      _sus = new SignUpSingleton(AppContext);
      _sus.UserSignUp();
      //_os.OrderedPizzas();
      // GetAllPizzas();
    }
    
    private static void GetAllPizzas()
    {
      foreach (var p in _ps.Get())
      {
        Console.WriteLine(p);
      }
    }
    

  }
}
