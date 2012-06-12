using System;
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