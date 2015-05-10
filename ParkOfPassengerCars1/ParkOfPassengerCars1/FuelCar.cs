using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkOfPassengerCars1
{

    class FuelCar : IPassengerCar, IFuelCarProperties
    {
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
            return string.Format("FuelCar: Brand {0}, Model {1}, Color {2}, Year {3}, ", Brand, Model, Color, CreateDate)
                + string.Format("FuelConsumption {0} л., Cost {1}, MaxSpeed {2} км/ч, Class {3}, ", FuelConsumption, Cost, MaxSpeed, CarClass)
                + string.Format("BodyType {0}, NumberOfPlaces {1}, TypeOfEngine {2}", Body, NumberOfPlaces, TypeOfEngine);

        }
    }
}
