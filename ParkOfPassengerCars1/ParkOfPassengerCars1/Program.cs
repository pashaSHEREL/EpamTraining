using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkOfPassengerCars1
{
    class Program
    {
        static void Main(string[] args)
        {
            TaxiStation station = new TaxiStation();

            station.Add(new ElectroCar() { Brand = CarBrand.BMW, MaxSpeed = 300, Cost = 15000, EnergyConsumption=12});
            station.Add(new ElectroCar() { Brand = CarBrand.AUDI, MaxSpeed = 250, Cost = 12000, EnergyConsumption = 14 });
            station.Add(new ElectroCar() { Brand = CarBrand.FORD, MaxSpeed = 200, Cost = 135000, EnergyConsumption = 9 });
            station.Add(new FuelCar() { Brand = CarBrand.KIA, FuelConsumption = 11, MaxSpeed = 270, Cost = 17000 });
            station.Add(new FuelCar() { Brand = CarBrand.LADA, FuelConsumption = 22, MaxSpeed = 320, Cost = 15000 });
            station.Add(new FuelCar() { Brand = CarBrand.LEXUS, FuelConsumption = 11, MaxSpeed = 180, Cost = 15000 });
            station.Add(new HybridCar() { Brand = CarBrand.MITSUBISHI, FuelConsumption = 4, MaxSpeed = 170, Cost = 35000 });
            station.Add(new HybridCar() { Brand = CarBrand.NISSAN, FuelConsumption=9, MaxSpeed = 250, Cost = 15000 });

            Console.WriteLine("BEFORE:");
            foreach (var item in station)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }

            station.SortByFuelConsumption();

            Console.WriteLine("AFTER:");
            foreach (var item in station)
            {

                Console.WriteLine(item);
                Console.WriteLine();
            }

            Console.WriteLine("------------------------------");

            station.Display();

            Console.WriteLine("------------------------------");

            Console.WriteLine("Results for:");
            bool i = false;

            foreach (var item in station.GetCarsBySpeed(150, 250))
            {
                i = true;
                Console.WriteLine(item);
                Console.WriteLine();
            }

            if (!i)
            {
                Console.WriteLine("order is empty");
            }
            Console.WriteLine();


            Console.WriteLine("Toatal cost: {0:C}",station.GetTotalCost());

            Console.ReadLine();

        }
    }
}
