using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;
using PizzaBox.Storing.Databases;
using System;

// Builds a Pizza
namespace PizzaBox.Client.Singletons
{
  public class PizzeriaSingleton
  {
    // private GenericRepository<Pizza> PizzaRepository;
    private PizzaRepository PizzaRepository;
    private static ApplicationDbContext Context;
    private static PizzeriaSingleton _ps;
    public PizzeriaSingleton() { }

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

    public List<Pizza> GetAll()
    {
      return PizzaRepository.GetAll();
    }

    public void Post(string name, Crust crust, Size size, List<Topping> toppingList, Order currentOrder)
    {
      // get pizza id
      var piz = new Pizza(){
        Order = currentOrder,
        Crust = crust,
        Size = size
      };
      PizzaRepository.Post(piz);
      piz = PizzaRepository.GetAll().Last();

      // set crust and size id for pizza & connect
      crust.Pizzas = new List<Pizza> { piz }; // p.crust = *crustId
      size.Pizzas = new List<Pizza> { piz };
      piz.Name = name;
      
      // add to pizza database
      PizzaRepository.Put(piz);
      piz = PizzaRepository.GetAll().Last();
      
      // set pizza topping to pizza & connect
      var PizTopRepo = new GenericRepository<PizzaTopping>(Context);

      foreach (var t in toppingList)
      {
        // get pizza topping id
        var PizzaToppingObject = new PizzaTopping()
        {
          Topping = t,
          Pizza = piz
        };
        PizTopRepo.Post(PizzaToppingObject);
        PizzaToppingObject = PizTopRepo.GetAll().Last();
        // PizzaToppingObject.Pizzas = new List<Pizza> { p };
        
        // add pizzatopping to list
        piz.PizzaToppings = new List<PizzaTopping> { PizzaToppingObject };
        
        // connect pizza topping and topping
        t.PizzaToppings = new List<PizzaTopping>{ PizzaToppingObject };

      }
    }
  }
}