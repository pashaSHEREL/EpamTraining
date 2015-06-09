using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CheckPoint3ATS
{
    class Program
    {
        static void ModelOfCall(ISubscriber subscriber, ISubscriber subscriber1)
        {
            subscriber.Call(subscriber1.PhoneNumber);
            subscriber1.Answer();
            subscriber.EndCall();
        }
        static void Main(string[] args)
        {
            Subscriber subscriber = new Subscriber("Sherel", "Pavel", "Popovicha") ;
            Subscriber subscriber1 = new Subscriber("Ivan", "Demin", "Popovicha");
            Subscriber subscriber2 = new Subscriber("Nastia", "Sherel", "Popovicha");
            Subscriber subscriber3 = new Subscriber("Robert", "Sherel", "Popovicha");

            ATS ats = new ATS(new List<IPort>() { 
                new Port(0), 
                new Port(1),
                new Port(2),
                new Port(3), }, 50);

            CompanyOperator company = new CompanyOperator(ats, "50");
            company.AddTerminal(new Terminal("Zte v 210", 10, 1 ));
            company.AddTerminal(new Terminal("Zte v 210", 10, 2));
            company.AddTerminal(new Terminal("Zte v 210", 10, 3));
            company.AddTerminal(new Terminal("Zte v 210", 10, 4));

            company.ConcludeContract(subscriber, 5040);
            company.ConcludeContract(subscriber1, 5041);
            company.ConcludeContract(subscriber2, 5042);
            company.ConcludeContract(subscriber3, 5043);

            //subscriber1.Call(5043);
            //subscriber1.Answer();
            //subscriber.Call(5041);
            
            
            subscriber.EndCall();
            
            Console.WriteLine("Port 0 {0}",ats.Ports[0].Mode);
            Console.WriteLine("Port 1 {0}",ats.Ports[1].Mode);
            Console.WriteLine("Port 2 {0}",ats.Ports[2].Mode);
            Console.WriteLine("Port 3 {0}",ats.Ports[3].Mode);
            
            Console.ReadLine();
        }
    }
}
