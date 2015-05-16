using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkOfPassengerCars1
{
    public interface IPassengerCar:ICar
    {
      ClassOfCar CarClass { get; }
      BodyTypeCar Body { get; }
      int NumberOfPlaces { get; } 
    }
}
