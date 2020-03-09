using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  public class User : AccountHolder
  {
    #region NAVIGATIONAL PROPERTIES
    public List<Order> Orders { get; set; }
    #endregion

    public User()
    {
      // Id = this.GetHashCode();
    }
  }
}