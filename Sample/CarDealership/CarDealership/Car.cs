using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;

namespace CarDealership
{
  [InheritedExport]
  public abstract class Car
  {
    public abstract void Drive();
  }
}
