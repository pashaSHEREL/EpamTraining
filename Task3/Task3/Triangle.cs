using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    enum TypeOfTriangle
    {  
        Rectangular,
        Obtuse,
        AcuteAngled
    }
    class Triangle
    {
        public double A{get;set;}
        public double B{get;set;}
        public double C{get;set;}
        private TypeOfTriangle typeOfTriangle;

        public Triangle()
        { }

        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        private void GetMaxSide(out double side1, out double side2, out double sideMax)
        {
           
            if ((A > B) && (A > C))
            {
                sideMax = A;
                side1 = B;
                side2 = C;
            }
            else
            {
                if ((B > A) && (B > C))
                {
                    sideMax = B;
                    side1 = A;
                    side2 = C;
                }
                else
                {
                    sideMax = C;
                    side1 = B;
                    side2 = A;
                }
            }

        }

        private void SetTypeOfTriangle(double side1, double side2, double sideMax)
        {

            if (Math.Pow(sideMax, 2) == Math.Pow(side1, 2) + Math.Pow(side2, 2))
            {
                typeOfTriangle = TypeOfTriangle.Rectangular;
            }
            else
            {
                if (Math.Pow(sideMax, 2) > Math.Pow(side1, 2) + Math.Pow(side2, 2))
                {
                    typeOfTriangle = TypeOfTriangle.Obtuse;
                }
                else
                {
                    typeOfTriangle = TypeOfTriangle.AcuteAngled;
                }
            }

        }

        private void TriangleTypeView()
        {

            switch (typeOfTriangle)
            {
                case TypeOfTriangle.Rectangular:
                    Console.WriteLine("Triangle is Rectangular");
                    break;
                case TypeOfTriangle.Obtuse:
                    Console.WriteLine("Triangle is Obtuse");
                    break;
                case TypeOfTriangle.AcuteAngled:
                    Console.WriteLine("Triangle is AcuteAngled");
                    break;
                default:
                    break;
            }

        }

        public void GetTypeOfTriangle()
        {
            double side1=0;
            double side2=0;
            double sideMax=0;

            if ((A + B > C) && (A + C > B) && (B + C > A))
            {

                GetMaxSide(out side1,out side2,out sideMax);

                SetTypeOfTriangle(side1,side2,sideMax);

                TriangleTypeView();

            }
            else 
            {
                Console.WriteLine("Triangle does not exist");
            }
        
        }

    }
}
