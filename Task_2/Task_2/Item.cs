using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_2
{
    class Item
    {
        private string name;
        private double cost;
        private double number;

        public Item()
        { 
        }

        public Item(double cost, double number)
            : this("AnyItem", cost, number)
        {}

        public Item(string name, double cost, double number)
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

        public double TotalCost( )
        {
            return cost * number;
        }
    }
}
