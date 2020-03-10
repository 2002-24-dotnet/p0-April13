using System;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Databases;
using PizzaBox.Storing.Repositories;
namespace PizzaBox.Client.Singletons
{
  public class LoginSingleton
  {
    private GenericRepository<User> UserRepository;
    private GenericRepository<Store> StoreRepository;
    private long AccountId;
    private static ApplicationDbContext Context;

    public LoginSingleton(ApplicationDbContext AppContext)
    {
      UserRepository = new GenericRepository<User>(AppContext);
      StoreRepository = new GenericRepository<Store>(AppContext);
      Context = AppContext;
    }

    public void login()
    {
      string input;
      bool Valid = false;
      bool exit = false;
      do
      {
        Console.Write("Are you a User or a Store? ");
        input = Console.ReadLine();
        input = input.Replace(" ", "").ToLower();
        if (input.Equals("user"))
          Valid = true;
        else if (input.Equals("store"))
          Valid = true;
        else if (input.Equals("exit"))
          exit = true;
        else
          Console.WriteLine("Enter \"user\" or \"store\".");
      }
      while (!Valid && !exit);

      if (!exit && Valid)
      {
        loginInfo(input);
      }
    }

    private void loginInfo(string input)
    {
      string email;
      string pw = "";
      bool found = false;
      bool exit = false;
      do
      {
        Console.Write("Email: ");
        email = Console.ReadLine();
        if (email.Equals("exit"))
          input = "exit";
        else
        {
          Console.Write("Password: ");
          pw = Console.ReadLine();
        }
        if (pw.Equals("exit"))
          input = "exit";

        switch (input)
        {
          case "user":
            found = UserExists(email, pw);
            Console.WriteLine("Login Successful!");
            UserSingleton UserLoggedIn = new UserSingleton(Context, AccountId);
            UserLoggedIn.Options();
            break;
          case "store":
            found = StoreExists(email, pw);
            Console.WriteLine("Login Successful!");
            StoreSingleton StoreLoggedIn = new StoreSingleton(Context, AccountId);
            StoreLoggedIn.Options();
            break;
          case "exit":
            exit = true;
            break;
          default:
            break;
        }

        if (!found && !exit)
          Console.WriteLine("Incorrect Login!");

      } while (!found && !exit);

      if (found && AccountId == 0)
      {
        switch (input)
        {
          case "user":
            UserSingleton LoginUser = new UserSingleton(Context, AccountId);
            LoginUser.Options();
            break;
          case "store":
            StoreSingleton LoginStore = new StoreSingleton(Context, AccountId);
            LoginStore.Options();
            break;
          default:
            break;
        }
      }
    }

    private bool UserExists(string email, string password)
    {
      if (StoreRepository == null)
      {
        Console.WriteLine("No Access to User Repository");
        return false;
      }
      foreach (var u in UserRepository.GetAll())
      {
        if (email.Equals(u.Email, StringComparison.OrdinalIgnoreCase) && password.Equals(u.Password))
        {
          AccountId = u.Id;
        }
        else
          return false;
      }
      return true;
    }
    private bool StoreExists(string email, string password)
    {
      if (StoreRepository == null)
      {
        Console.WriteLine("No Access to Store Repository");
        return false;
      }
      foreach (var s in StoreRepository.GetAll())
      {
        if (email.Equals(s.Email, StringComparison.OrdinalIgnoreCase) && password.Equals(s.Password))
        {
          AccountId = s.Id;
        }
        else
          return false;
      }
      return true;
    }
  }
}