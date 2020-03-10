using System;
using PizzaBox.Client.Singletons;
using PizzaBox.Storing.Databases;

namespace PizzaBox.Client
{
  class Program
  {
    private static LoginSingleton _ls; 
    private static ApplicationDbContext AppContext = new ApplicationDbContext();
    private static SignUpSingleton _sus;

    static void Main(string[] args)
    {
      _sus = new SignUpSingleton(AppContext);
      _sus.CreateUserAndStore(); 

      _ls = new LoginSingleton(AppContext);
      _ls.login();
      
    }
    
  }
}
