using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ParkOfPassengerCars1
{
    class TaxiStation : IList<ICar>
    {
        private IList<ICar> transport = new List<ICar>();

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

            transport = (from t in transport where t is IFuelCarProperties orderby ((IFuelCarProperties)t).FuelConsumption descending select t).
                Concat(from t in transport where (t is IElectroCarProperties) && (!(t is IFuelCarProperties)) orderby ((IElectroCarProperties)t).EnergyConsumption descending select t).ToList<ICar>();
        }

        public double GetTotalCost()
        {
            double sum=0;

            foreach (var item in transport)
	        {
		        sum+=item.Cost;
	        }
            return sum;
        }
        
        public int IndexOf(ICar item)
        {
            return transport.IndexOf(item);
        }

        public void Insert(int index, ICar item)
        {
            transport.Insert(index,item);
        }

        public void RemoveAt(int index)
        {
            transport.RemoveAt(index);
        }

        public ICar this[int index]
        {
            get
            {
                return (transport.ToList())[index];
            }
            set
            {
                this.Insert(index, value);
            }
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
            transport.CopyTo(array, arrayIndex);
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
