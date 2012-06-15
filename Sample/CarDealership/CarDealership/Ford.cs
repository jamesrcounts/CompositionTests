using System;
using System.ComponentModel.Composition;

namespace CarDealership
{
    public interface IMotor
    {
    }

    [Export(typeof(Car))]
    public class Ford : Car
    {
        [Import]
        public IMotor Motor { get; set; }

        public override void Drive()
        {
            throw new NotImplementedException();
        }
    }

    [Export(typeof(IMotor))]
    public class V8Motor : IMotor
    {
    }
}