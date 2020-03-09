using System.Collections.Generic;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
  public abstract class APizzaComponent : IEntity
  {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public long Id { get; set; }
    public List<Pizza> Pizzas { get; set; }

  }
}
