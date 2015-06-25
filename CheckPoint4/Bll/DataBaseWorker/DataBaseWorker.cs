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
                _directory.Create();
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

            foreach (var csvObject in _listCsvObjects)
            {
                orderRepository.Add(new Order()
                {
                    Date = csvObject.Date,
                    CustomerId = csvObject.CustomerId
                });
                orderRepository.Save();

                Item item = new ItemRepository().GetRecord(csvObject.ItemId);
                Order order = new OrderRepository().GetAll().LastOrDefault();

                quantity = null;

                if (item.Cost != null && item.Cost != 0)
                {
                    quantity = csvObject.TotalCost/item.Cost;
                }

                if (order != null && item != null)
                {
                    orderItemRepository.Add(new OrderItem()
                    {
                        OrderId = order.OrderId,
                        ItemId = item.ItemId,
                        Quantity = quantity
                    });
                }
            }
            orderItemRepository.Save();
        }

        protected void StartReadFiles()
        {
            Task.WaitAll(GetFilesNames().Select(item => Task.Factory.StartNew(() => { ReadFile(item); })).ToArray());
        }

        protected List<string> GetFilesNames()
        {
            return _directory.GetFiles().Select(item => item.FullName).ToList();
        }

        protected void ReadFile(string fileFullName)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(Task.CurrentId.GetValueOrDefault());

            using (TextFieldParser parser = new TextFieldParser(fileFullName))
            {
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                string[] strBuf = new string[4];

                while (!parser.EndOfData)
                {
                    strBuf = parser.ReadFields();
                    _listCsvObjects.Add(new ObjectFromCsv()
                    {
                        Date = DateTime.Parse(strBuf[0]),
                        CustomerId = int.Parse(strBuf[1]),
                        ItemId = int.Parse(strBuf[2]),
                        TotalCost = int.Parse(strBuf[3])
                    });
                }
            }
        }
    }
}