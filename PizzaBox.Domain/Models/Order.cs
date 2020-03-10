using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
  public class Order
  {
    public long OrderId { get; set; }

    #region NAVIGATIONAL PROPERTIES
    public List<Pizza> Pizzas { get; set; }
    public User User { get; set; }
    public Store Store { get; set; }

    #endregion
    public decimal Price
    {
      get
      {
        decimal price = 0;;
        if (Pizzas == null)
        {
          return price;
        }
        foreach(var p in Pizzas)
        {
          price += p.Price;
        }
        return price;
        
      }
    }
    public DateTime date;

    public Order()
    {
      // OrderId = DateTime.Now.Ticks;
      date = DateTime.Now;
    }
  }
}