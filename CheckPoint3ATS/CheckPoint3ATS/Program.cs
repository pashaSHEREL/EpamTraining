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
            subscriber.HangUpPhone();
        }
        static void Main(string[] args)
        {
            Time time = new Time(new DateTime(2015,1,1));
            Subscriber subscriber = new Subscriber("Sherel", "Pavel", "Popovicha") ;
            Subscriber subscriber1 = new Subscriber("Ivan", "Demin", "Popovicha");
            Subscriber subscriber2 = new Subscriber("Nastia", "Sherel", "Popovicha");
            Subscriber subscriber3 = new Subscriber("Robert", "Sherel", "Popovicha");

            ATS ats = new ATS(50,time.Days);
            ats.AddPort(new Port(ats.Ports.Count));
            ats.AddPort(new Port(ats.Ports.Count));
            ats.AddPort(new Port(ats.Ports.Count));
            ats.AddPort(new Port(ats.Ports.Count));

            CompanyOperator company = new CompanyOperator(ats, "50",time);
            company.AddTerminal(new Terminal("Zte v 210", 10, 1 ));
            company.AddTerminal(new Terminal("Zte v 210", 10, 2));
            company.AddTerminal(new Terminal("Zte v 210", 10, 3));
            company.AddTerminal(new Terminal("Zte v 210", 10, 4));

            company.ConcludeContract(subscriber, 5040);
            company.ConcludeContract(subscriber1, 5041);
            company.ConcludeContract(subscriber2, 5042);
            company.ConcludeContract(subscriber3, 5043);

            time.ChangeTime(2);
            //subscriber1.DisconnectFromPort();
            subscriber1.Call(5042);
            subscriber2.Answer();
            subscriber1.HangUpPhone();
            foreach (var variable in subscriber1.GetStatistic(new DateTime(2014, 1, 1), new DateTime(2016, 1, 1)))
            {
                Console.WriteLine((variable as CallInfoForBilling).DayCall);
            }
            
           // subscriber1.HangUpPhone();
            //subscriber1.Call(5043);
           // subscriber3.Answer();
            //subscriber.Call(5041);
            time.ChangeTime(2);
           //сделать:получать статистику в subscr, подключиться к порту в subscr, из порта убрать номер телефона

            subscriber.HangUpPhone();
            
            Console.WriteLine("Port 0 {0}",ats.Ports[0].Mode);
            Console.WriteLine("Port 1 {0}",ats.Ports[1].Mode);
            Console.WriteLine("Port 2 {0}",ats.Ports[2].Mode);
            Console.WriteLine("Port 3 {0}",ats.Ports[3].Mode);
            
            Console.ReadLine();
        }
    }
}
