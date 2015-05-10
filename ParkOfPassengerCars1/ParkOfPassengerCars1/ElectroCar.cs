using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkOfPassengerCars1
{

    class ElectroCar:IPassengerCar,IElectroCarProperties
    {
        public int PowerReserv { get; set; }
        public TimeSpan ChargingTime { get; set; }

        public double EnergyConsumption
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
            return string.Format("ElectroCar: Brand {0}, Model {1}, Color {2}, Year {3}, ", Brand, Model, Color, CreateDate)
                + string.Format("EnergyConsumption {0} кВт/км, Cost {1:c}, MaxSpeed {2} км/ч, Class {3}, ", EnergyConsumption, Cost, MaxSpeed, CarClass)
                + string.Format("BodyType {0}, NumberOfPlaces {1}, PowerReserv {2} км, ChargingTime {3}", Body, NumberOfPlaces, PowerReserv, ChargingTime);
            
        }
    }
}
