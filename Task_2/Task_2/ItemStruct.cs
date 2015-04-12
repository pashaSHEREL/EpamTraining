using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_2
{
    struct ItemStruct
    {
        private string name;
        private double cost;
        private double number;

        public ItemStruct(double cost, double number)
            : this("AnyItem", cost, number)
        {}

        public ItemStruct(string name, double cost, double number)
        {
            this.name = name;
            this.cost = cost;
            this.number = number;
        }

        public string Name
        {
            get { return name;}
            set { name = value; }
        }

        public double Cost
        {
            get { return cost;}
            set { cost = value; }
        }
        
        public double Number
        {
            get { return number; }
            set { number = value; }
        }

        public double TotalCost()
        {
            return cost * number;
        }
    }
}
