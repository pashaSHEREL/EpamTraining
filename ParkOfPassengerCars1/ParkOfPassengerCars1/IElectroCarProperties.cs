using System;
namespace ParkOfPassengerCars1
{
    interface IElectroCarProperties
    {
        TimeSpan ChargingTime { get; }
        int PowerReserv { get; }
        double EnergyConsumption { get; }

    }
}
