using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  public class Topping : APizzaComponent
  {
    public List<PizzaTopping> PizzaToppings { get; set; }
    public Topping()
    {
      Id = this.GetHashCode();
    }
  }
}
