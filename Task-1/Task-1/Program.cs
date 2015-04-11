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

            LinearFunction f = new LinearFunction(-3, 4);
            Console.WriteLine("Enter the number x");
            double res;
            var b = Double.TryParse(Console.ReadLine(), out res);

            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Value of the function f={0}*x+{1}={2}", f.A,f.B,f.GetY(x));
            Console.ReadLine();
        }
    }
}
