using System.ComponentModel.Composition;

namespace CarDealership
{
  [InheritedExport]
  public abstract class Car
  {
    public abstract void Drive();
  }
}