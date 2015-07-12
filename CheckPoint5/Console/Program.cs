using System.Collections.Generic;
using System.IO;
using Bll;
using System.Threading.Tasks;
using System.Configuration;


namespace Console
{

    internal class Program
    {
        private static void Main(string[] args)
        {
            string directoryForRead = ConfigurationManager.AppSettings["directoryForRead"];
            string directoryMoveTo = ConfigurationManager.AppSettings["directoryMoveTo"];
            DataBaseWorker worker = new DataBaseWorker(directoryForRead, directoryMoveTo);

            DirectoryInfo directory = new DirectoryInfo(directoryForRead);
            List<Task> tasks = new List<Task>();

            foreach (var item in directory.GetFiles())
            {
                tasks.Add(Task.Factory.StartNew(worker.AddAllInDataBase, item.FullName));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}