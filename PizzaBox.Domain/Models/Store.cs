using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  public class Store : AccountHolder
  {
    #region NAVIGATIONAL PROPERTIES
    public List<Order> Orders { get; set; }
    #endregion

    public Store()
    {
      // Id = this.GetHashCode();
    }
  }
}