using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    class Program
    {

        static double ValidationChecks()
        {
            double side=0;
            bool b = double.TryParse(Console.ReadLine(), out side);

            while (!b)
            {
                Console.WriteLine("Enter a number");
                b = double.TryParse(Console.ReadLine(), out side);         
            }

            return side;
        }

        static void Main(string[] args)
        {
            double side1=0;
            double side2=0;
            double side3=0;

            Console.WriteLine("Enter side 1 of the triangle");
            side1=ValidationChecks();
            Console.WriteLine("Enter side 2 of the triangle");
            side2=ValidationChecks();
            Console.WriteLine("Enter side 3 of the triangle");
            side3=ValidationChecks();
            
            Triangle triangle = new Triangle(side1,side2,side3);
            triangle.GetTypeOfTriangle();

            Console.ReadLine();
        }
    }
}
