using System;
namespace ParkOfPassengerCars1
{
    interface IFuelCarProperties
    {
        TypeOfFuel TypeOfEngine { get; }
        double FuelConsumption { get; }
    }
}
