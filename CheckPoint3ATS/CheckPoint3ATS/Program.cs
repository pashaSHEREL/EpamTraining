using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CheckPoint3ATS
{
    class Program
    {
        static void Main(string[] args)
        {
            Subscriber subscriber = new Subscriber("Sherel", "Pavel", "Popovicha") ;
            Subscriber subscriber1 = new Subscriber("Ivan", "Demin", "Popovicha");
            Subscriber subscriber2 = new Subscriber("Nastia", "Sherel", "Popovicha");
            Subscriber subscriber3 = new Subscriber("Robert", "Sherel", "Popovicha");
           
            ATS ats = new ATS(new List<IPort>() { 
                new Port(){ Number=0}, 
                new Port(){ Number=1},
                new Port(){ Number=2},
                new Port(){ Number=3}}, 50);

            CompanyOperator company = new CompanyOperator(ats, "50");
            company.Terminals.Add(new Terminal() { TerminalId = 1 });
            company.Terminals.Add(new Terminal() { TerminalId = 2 });
            company.Terminals.Add(new Terminal() { TerminalId = 3 });
            company.Terminals.Add(new Terminal() { TerminalId = 4 });

            company.ConcludeContract(subscriber, 5040);
            company.ConcludeContract(subscriber1, 5041);
            company.ConcludeContract(subscriber2, 5042);
            company.ConcludeContract(subscriber3, 5043);

           // subscriber.Call(5042);
            //subscriber1.Call(5043);
            subscriber2.Answer();
           //subscriber.EndCall();
            
            Console.WriteLine("Port 0 {0}",ats.Ports[0].Mode);
            Console.WriteLine("Port 1 {0}",ats.Ports[1].Mode);
            Console.WriteLine("Port 2 {0}",ats.Ports[2].Mode);
            Console.WriteLine("Port 3 {0}",ats.Ports[3].Mode);
            
            Console.ReadLine();
        }
    }
}
