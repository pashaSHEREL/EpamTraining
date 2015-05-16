using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkOfPassengerCars1
{
    public interface ICar
    {
        CarBrand Brand { get; }
        DateTime CreateDate { get; }
        CarColor Color { get; }
        double FuelConsumption { get; }
        string Model { get; }
        double Cost { get; }
        int MaxSpeed { get; }
    }
}
