using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  public class Size : APizzaComponent
  {
    public Size()
    {
      Id = this.GetHashCode();
    }
  }
}
