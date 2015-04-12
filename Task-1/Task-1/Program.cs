using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {

            LinearFunction linearFunction = new LinearFunction(-3, 4);
            double res;
            Console.WriteLine("Enter the number x");
            bool b = Double.TryParse(Console.ReadLine(), out res);

            while (!b)
            {
                Console.WriteLine("Enter the double number x");
                b = Double.TryParse(Console.ReadLine(), out res);
            }
            Console.WriteLine("Value of the function linearFunction={0}*x+{1}={2}", linearFunction.A, linearFunction.B, linearFunction.GetY(res));
            Console.ReadLine();
        }
    }
}
