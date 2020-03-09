using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  public class Crust : APizzaComponent
  {
    public Crust()
    {
      Id = this.GetHashCode();
    }
  }
}
