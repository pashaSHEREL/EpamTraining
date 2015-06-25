using Bll;

namespace Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bll.IDataBaseWorker worker = new DataBaseWorker(@"d:\test1\", @"d:\test2\");
            worker.AddAllInDataBase();
        }
    }
}