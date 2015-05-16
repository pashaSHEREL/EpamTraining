using System;
namespace ParkOfPassengerCars1
{
    public interface IElectroCarProperties
    {
        TimeSpan ChargingTime { get; }
        int PowerReserv { get; }
        double EnergyConsumption { get; }

    }
}
