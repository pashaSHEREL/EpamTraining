using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using Models;
using Microsoft.VisualBasic.FileIO;

namespace Bll
{
    public class DataBaseWorker : IDataBaseWorker
    {
        private List<ObjectFromCsv> _listCsvObjects = new List<ObjectFromCsv>();
        private readonly DirectoryInfo _directory;
        private readonly DirectoryInfo _directoryForReadFiles;

        public DataBaseWorker(string directory, string directoryForReadFiles)
        {
            _directory = new DirectoryInfo(directory);
            if (!_directory.Exists)
            {
                throw new Exception("Directory does not exist.");
            }

            _directoryForReadFiles = new DirectoryInfo(directoryForReadFiles);
            if (!_directoryForReadFiles.Exists)
            {
                _directoryForReadFiles.Create();
            }
        }

        public void AddAllInDataBase()
        {
            StartReadFiles();

            foreach (var item in _directory.GetFiles())
            {
                item.MoveTo(string.Format("{0}{1}", _directoryForReadFiles, item));
            }

            OrderRepository orderRepository = new OrderRepository();
            OrderItemRepository orderItemRepository = new OrderItemRepository();
            int? quantity = null;
            Item itemObj=new Item();
            Order orderObj=new Order();

            foreach (var csvObject in _listCsvObjects)
            {
                orderRepository.Add(new Order()
                {
                    Date = csvObject.Date,
                    CustomerId = csvObject.CustomerId
                });
                orderRepository.Save();

                itemObj = new ItemRepository().GetRecord(csvObject.ItemId);
                orderObj = new OrderRepository().GetAll().LastOrDefault();

                quantity = null;

                if (itemObj.Cost != null && itemObj.Cost != 0)
                {
                    quantity = csvObject.TotalCost / itemObj.Cost;
                }

                if (orderObj != null && itemObj != null)
                {
                    orderItemRepository.Add(new OrderItem()
                    {
                        OrderId = orderObj.OrderId,
                        ItemId = itemObj.ItemId,
                        Quantity = quantity
                    });
                }
            }
            orderItemRepository.Save();
        }

        protected void StartReadFiles()
        {
            List<Task<IEnumerable<ObjectFromCsv>>> tasks = new List<Task<IEnumerable<ObjectFromCsv>>>();

            foreach (var item in GetFilesNames())
            {
                tasks.Add(Task.Factory.StartNew<IEnumerable<ObjectFromCsv>>(ReadFile, item));
            }

            _listCsvObjects = tasks.SelectMany(x => x.Result).ToList();
           
        }

        protected List<string> GetFilesNames()
        {
            return _directory.GetFiles().Select(item => item.FullName).ToList();
        }

        protected IEnumerable<ObjectFromCsv> ReadFile(object fileFullName)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(Task.CurrentId.GetValueOrDefault());
            List<ObjectFromCsv> buf = new List<ObjectFromCsv>();

            using (TextFieldParser parser = new TextFieldParser((string) fileFullName))
            {
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                string[] strBuf = new string[4];

                while (!parser.EndOfData)
                {
                    strBuf = parser.ReadFields();
                    buf.Add(new ObjectFromCsv()
                    {
                        Date = DateTime.Parse(strBuf[0]),
                        CustomerId = int.Parse(strBuf[1]),
                        ItemId = int.Parse(strBuf[2]),
                        TotalCost = int.Parse(strBuf[3])
                    });
                }
                return buf;
            }
        }
    }
}