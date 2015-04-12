using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace Task_2
{
    class Program
    {
        static public double GetSumOfItem()
        {
            const int c = 100000;
            Random random = new Random();
            ArrayList mas= new ArrayList(c);
            double sum = 0;
            for (int i = 0; i < c; i++)
            {
                mas.Add(new Item((double)random.Next(100), (double)random.Next(100)));
                sum += ((Item)mas[i]).TotalCost();
            }
            return sum;
        }

        static public double GetSumOfItemStruct()
        {
            const int c = 100000;
            Random random = new Random();
            ArrayList mas = new ArrayList(c);
            double sum = 0;
            for (int i = 0; i < c; i++)
            {
                mas.Add(new ItemStruct((double)random.Next(100), (double)random.Next(100)));
                sum += ((ItemStruct)mas[i]).TotalCost();
            }
            return sum;
        }

        static void Main(string[] args)
        {

            Stopwatch timer = new Stopwatch();

            timer.Start();
            double sum=GetSumOfItem();
            timer.Stop();

            Console.WriteLine("full cost={0}, time for class = {1}", sum.ToString("c", CultureInfo.CreateSpecificCulture("ru-RUS")), timer.Elapsed.TotalSeconds);

            timer.Start();
            sum = GetSumOfItemStruct();
            timer.Stop();

            Console.WriteLine("full cost={0}, time for struct = {1}", sum.ToString("c",CultureInfo.CreateSpecificCulture("ru-RUS")), timer.Elapsed.TotalSeconds);

            Console.ReadLine();
            /*    IL_0039:  box        Task_2.ItemStruct
                  IL_003e:  callvirt   instance int32 [mscorlib]System.Collections.ArrayList::Add(object)
                  IL_0043:  pop
                  IL_0044:  ldloc.2
                  IL_0045:  ldloc.1
                  IL_0046:  ldloc.3
                  IL_0047:  callvirt   instance object [mscorlib]System.Collections.ArrayList::get_Item(int32)
                  IL_004c:  unbox.any  Task_2.ItemStruct
                Так как структура - тип по значению, то при вызове кода 
              (mas.Add(new ItemStruct((double)random.Next(100), (double)random.Next(100)));
              sum += ((ItemStruct)mas[i]).TotalCost();) происходит запаковка и распаковка данных, за счет этого расчет общей
              стоимости товаров происходит дольше, проблема решается использованием обобщенных типов (List<>).
            */

        }
    }
}
