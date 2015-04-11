using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_1
{
    class LinearFunction
    {
        private double a;
        private double b;

        public LinearFunction()
        {
        }

        public LinearFunction(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public double A
        {
            get { return a; }
            set { a = value; }
        }

        public double B
        {
            get { return b; }
            set { b = value; }
        }

        public double GetY(double x)
        {
            return a * x + b;
        }
    }
}
