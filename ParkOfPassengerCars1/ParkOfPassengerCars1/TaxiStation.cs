using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ParkOfPassengerCars1
{
    public class TaxiStation : ICollection<ICar>
    {
        private List<ICar> transport = new List<ICar>();

        public void Display()
        {
            foreach (var item in transport)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public IEnumerable<ICar> GetCarsBySpeed(int firstSpeed, int lastSpeed)
        {
            foreach (var item in transport)
            {
                if (item.MaxSpeed >= firstSpeed && item.MaxSpeed <= lastSpeed)
                {
                    yield return item;
                }
            }
        }

        public void SortByFuelConsumption()
        {

            transport = (from t in transport orderby t.FuelConsumption descending select t).ToList();
        }

        public double GetTotalCost()
        {
            return transport.Sum<ICar>(t=>t.Cost);
        }

        public void Add(ICar item)
        {
            transport.Add(item);
        }

        public void Clear()
        {
            transport.Clear();
        }

        public bool Contains(ICar item)
        {
            return transport.Contains(item);
        }

        public void CopyTo(ICar[] array, int arrayIndex)
        {
            transport.CopyTo(array,arrayIndex);
        }

        public int Count
        {
            get { return transport.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ICar item)
        {
            return transport.Remove(item);
        }

        public IEnumerator<ICar> GetEnumerator()
        {
            return transport.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
