using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            RepositoryForCustomer customer = new RepositoryForCustomer();
            //customer.Add(new ObjectCustomer() {FirstName = "Pasha"});
           // customer.Save();
            var l= customer.GetAll();
            foreach (var item in l)
            {
                Console.WriteLine("{0}  {1}",item.CustomerId,item.FirstName);
            }

           customer.Update(l[1], new ObjectCustomer(){FirstName = "Alf"});

           // customer.Delete(l[0]);
            customer.Save();
            Console.WriteLine("\n\n");
            l = customer.GetAll();
            foreach (var item in l)
            {
                Console.WriteLine("{0}  {1}", item.CustomerId, item.FirstName);
            }

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
