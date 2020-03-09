using System;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Databases;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.Singletons
{
  public class SignUpSingleton
  {
    private ApplicationDbContext Context;
    private GenericAccountRepository<User> UserRepository;
    
    public SignUpSingleton(ApplicationDbContext AppContext)
    {
      Context = AppContext;
      UserRepository = new GenericAccountRepository<User>(AppContext);
    }
    
    public void UserSignUp()
    {
      var NewUser = new User()
      {
        Name = "TestName",
        Email = null,
        Password = null,
        Address = null,
        PhoneNumber = null
      };
      UserRepository.Post(NewUser);
      Context.SaveChanges();
    }

    public void StoreSignUp()
    {
      var StoreRepo = new GenericAccountRepository<Store>(Context);
      var NewStore = new Store()
      {
        Name = "TestStoreName"
      };
      StoreRepo.Post(NewStore);
      Context.SaveChanges();
    }
  }
}