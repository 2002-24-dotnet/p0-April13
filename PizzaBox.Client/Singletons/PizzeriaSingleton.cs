using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;
using PizzaBox.Storing.Databases;

// Builds a Pizza
namespace PizzaBox.Client.Singletons
{
  public class PizzeriaSingleton
  {
    // private GenericRepository<Pizza> PizzaRepository;
    private PizzaRepository PizzaRepository;
    private static ApplicationDbContext Context;
    private static PizzeriaSingleton _ps;
    public PizzeriaSingleton(){}

    public PizzeriaSingleton(ApplicationDbContext AppContext)
    {
      // PizzaRepository = new GenericRepository<Pizza>(AppContext);
      
      Context = AppContext;
      PizzaRepository = new PizzaRepository(AppContext);
      // _ps = new PizzeriaSingleton(AppContext);
    }
    /*
    public PizzeriaSingleton(GenericRepository<Pizza> repo)
    {
      PizzaRepository = repo;
    }
    */

    public static PizzeriaSingleton Instance
    {
      get
      {
        return _ps;
      }
    }

    public List<Pizza> Get()
    {
      return PizzaRepository.GetAll();
    }

    public bool Post(Crust crust, Size size, List<Topping> toppings)
    {
      var p = new Pizza();

      crust.Pizzas = new List<Pizza> { p }; // p.crust = *crustId
      size.Pizzas = new List<Pizza> { p };

      return PizzaRepository.Post(p);
    }
  }
}