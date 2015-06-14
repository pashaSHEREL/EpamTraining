using System;


namespace CheckPoint3ATS
{
    internal class Program
    {
        private static void ModelOfCalls(ISubscriber subscriber, ISubscriber subscriber1, int numberOfCalls, Time time)
        {
            for (int i = 0; i < numberOfCalls; i++)
            {
                subscriber.Call(subscriber1.PhoneNumber);
                subscriber1.Answer();
                subscriber.HangUpPhone();
                time.ChangeTime(2);
            }
        }

        private static void Main(string[] args)
        {
            Time time = new Time(new DateTime(2015, 1, 1));
            Subscriber subscriber = new Subscriber("Sherel", "Pavel", "Popovicha");
            Subscriber subscriber1 = new Subscriber("Ivan", "Demin", "Popovicha");
            Subscriber subscriber2 = new Subscriber("Nastia", "Sherel", "Popovicha");
            Subscriber subscriber3 = new Subscriber("Robert", "Sherel", "Popovicha");

            ATS ats = new ATS(50, time.Days);
            ats.AddPort(new Port(ats.Ports.Count));
            ats.AddPort(new Port(ats.Ports.Count));
            ats.AddPort(new Port(ats.Ports.Count));
            ats.AddPort(new Port(ats.Ports.Count));

            CompanyOperator company = new CompanyOperator(ats, "Company", time);
            company.AddTerminal(new Terminal("Zte v 210", 10, 1));
            company.AddTerminal(new Terminal("Zte v 210", 10, 2));
            company.AddTerminal(new Terminal("Zte v 210", 10, 3));
            company.AddTerminal(new Terminal("Zte v 210", 10, 4));

            company.ConcludeContract(subscriber, 5040, new TalkingTariffPlan() {PricePerMinute = 10, FreeMinute = 100});
            company.ConcludeContract(subscriber1, 5041, new StandartTariffPlan() {PricePerMinute = 10});
            company.ConcludeContract(subscriber2, 5042, new StandartTariffPlan() {PricePerMinute = 10});
            company.ConcludeContract(subscriber3, 5043, new StandartTariffPlan() {PricePerMinute = 10});

            ModelOfCalls(subscriber, subscriber1, 15, time);
            subscriber.Pay(10000000);
            subscriber1.HangUpPhone();
            ModelOfCalls(subscriber, subscriber1, 15, time);
            subscriber.ViewStatisticFilterByDate(new DateTime(2014, 1, 1), new DateTime(2017, 1, 1));

            subscriber.ChangeTariffPlan(new StandartTariffPlan() {PricePerMinute = 10});
            subscriber.ChangeTariffPlan(new StandartTariffPlan() {PricePerMinute = 10});

            Console.WriteLine("Port 0 {0}", ats.Ports[0].Mode);
            Console.WriteLine("Port 1 {0}", ats.Ports[1].Mode);
            Console.WriteLine("Port 2 {0}", ats.Ports[2].Mode);
            Console.WriteLine("Port 3 {0}", ats.Ports[3].Mode);

            Console.ReadLine();
        }
    }
}