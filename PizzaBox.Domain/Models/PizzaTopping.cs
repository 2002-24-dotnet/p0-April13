using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;

namespace PizzaBox.Domain.Models
{
  public class PizzaTopping : IEntity
  {

    #region NAVIGATIONAL PROPERTIES
    
    public Topping Topping { get; set; }
    public Pizza Pizza { get; set; }

    #endregion
    public long Id { get; set; }
  }
}
