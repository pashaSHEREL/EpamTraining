using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkOfPassengerCars1
{ 
    public class HybridCar : IPassengerCar, IElectroCarProperties, IFuelCarProperties
    {
        public double EnergyConsumption
        {
            get;
            set;
        }

        public TimeSpan ChargingTime
        {
            get;
            set;
        }

        public int PowerReserv
        {
            get;
            set;
        }

        public TypeOfFuel TypeOfEngine
        {
            get;
            set;
        }

        public ClassOfCar CarClass
        {
            get;
            set;
        }

        public BodyTypeCar Body
        {
            get;
            set;
        }

        public int NumberOfPlaces
        {
            get;
            set;
        }

        public CarBrand Brand
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }

        public CarColor Color
        {
            get;
            set;
        }

        public string Model
        {
            get;
            set;
        }

        public double FuelConsumption
        {
            get;
            set;
        }

        public double Cost
        {
            get;
            set;
        }

        public int MaxSpeed
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("HybridCar: Brand {0}, Model {1}, Color {2}, Year {3}, ", Brand, Model, Color, CreateDate)
                + string.Format("FuelConsumption {0} л., EnergyConsumption {1} кВт/км, Cost {2:c}, MaxSpeed {3} км/ч, Class {4}, ", FuelConsumption, EnergyConsumption, Cost, MaxSpeed, CarClass)
                + string.Format("BodyType {0}, NumberOfPlaces {1}, PowerReserv {2} км, ChargingTime {3}, TypeOfEngine {4}", Body, NumberOfPlaces, PowerReserv, ChargingTime, TypeOfEngine);

        }

    }
}
