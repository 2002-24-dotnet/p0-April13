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

    private static PizzeriaSingleton _ps;
    // private GenericRepository<Pizza> PizzaRepository;

    private static OrderSingleton _os;
    private static ApplicationDbContext Context;

    public OrderSingleton(ApplicationDbContext AppContext)
    {
      
      //CrustRepository = new GenericRepository<Crust>(AppContext);
      //SizeRepository = new GenericRepository<Size>(AppContext);
      //ToppingRepository = new GenericRepository<Topping>(AppContext);
      
      // PizzaRepository = new GenericRepository<Pizza>(AppContext);
      _ps = new PizzeriaSingleton(AppContext);      
      Context = AppContext;
      // _os = new OrderSingleton(AppContext);
    }
    public OrderSingleton(GenericRepository<Crust> CrustRepo, GenericRepository<Size> SizeRepo, GenericRepository<Topping> ToppingRepo)
    {
      CrustRepository = CrustRepo;
      SizeRepository = SizeRepo;
      ToppingRepository = ToppingRepo;
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

    public void OrderedPizzas()
    {
      var crusts = new GenericRepository<Crust>(Context).GetAll();
      var sizes = new GenericRepository<Size>(Context).GetAll();
      //var sizes = SizeRepository.GetAll();

      if(_ps.Post(crusts[0], sizes[0], null))
        Console.WriteLine("Pizza Ordered");
      else
        Console.WriteLine("Pizza Not Ordered");
    }
  }
}