using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CarDealership
{
  [Export]
  public class Dealership
  {
    [ImportMany]
    public IEnumerable<Car> Cars { get; set; }
  }
}