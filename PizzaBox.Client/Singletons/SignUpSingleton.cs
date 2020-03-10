using System;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Databases;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.Singletons
{
  public class SignUpSingleton
  {
    private ApplicationDbContext Context;
    private GenericRepository<User> UserRepository;
    private GenericRepository<Store> StoreRepository;

    public SignUpSingleton(ApplicationDbContext AppContext)
    {
      Context = AppContext;
      UserRepository = new GenericRepository<User>(AppContext);
      StoreRepository = new GenericRepository<Store>(AppContext);
    }

    public void CreateUserAndStore()
    {
      if (UserRepository.GetAll().Count == 0)
      {
        UserSignUp("Person1", "Person1@mail.com", "Password123", "123 Main St, Arlington, TX 76543", "972-123-4567");
        StoreSignUp("Store1", "Store1@mail.com", "Password123", "456 Main St, Arlington, TX 76543", "817-123-4567");
        StoreSignUp("Store2", "Store2@mail.com", "Password123", "789 Main St, Arlington, TX 76543", "817-654-3210");
      }

    }
    public void UserSignUp(string n, string e, string pa, string a, string ph)
    {
      var NewUser = new User()
      {
        Name = n,
        Email = e,
        Password = pa,
        Address = a,
        PhoneNumber = ph
      };
      UserRepository.Post(NewUser);
    }

    public void StoreSignUp(string n, string e, string pa, string a, string ph)
    {
      var NewStore = new Store()
      {
        Name = n,
        Email = e,
        Password = pa,
        Address = a,
        PhoneNumber = ph
      };
      StoreRepository.Post(NewStore);
    }
  }
}