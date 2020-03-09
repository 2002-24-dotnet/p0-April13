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
    public Order()
    {
      // OrderId = DateTime.Now.Ticks;
    }
  }
}