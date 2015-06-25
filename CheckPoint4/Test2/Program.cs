using Bll;

namespace Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DataBaseWorker dataBaseWorker = new DataBaseWorker(@"d:\test5\", @"d:\test6\");
            dataBaseWorker.AddAllInDataBase();
            System.Console.ReadLine();
        }
    }
}