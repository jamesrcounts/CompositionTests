using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;

namespace CarDealership
{
  public class Mazda : Car
  {
    [Import]
    public Motor Engine { get; set; }

    public override void Drive()
    {
      throw new NotImplementedException();
    }
  }
}
