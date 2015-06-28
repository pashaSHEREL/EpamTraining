using Bll;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataBaseWorker worker = new DataBaseWorker(@"d:\test1\", @"d:\test2\");
            worker.AddAllInDataBase();
        }
    }
}
