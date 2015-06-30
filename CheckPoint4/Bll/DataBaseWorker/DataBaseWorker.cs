using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL;
using DAL.Models;
using Microsoft.VisualBasic.FileIO;

namespace Bll
{
    public class DataBaseWorker : IDataBaseWorker
    {
        private readonly DirectoryInfo _directoryForReadFile;
        private readonly DirectoryInfo _directoryMoveToFiles;

        public DataBaseWorker(string directory, string directoryForReadFiles)
        {
            _directoryForReadFile = new DirectoryInfo(directory);
            if (!_directoryForReadFile.Exists)
            {
                throw new Exception("Directory does not exist.");
            }

            _directoryMoveToFiles = new DirectoryInfo(directoryForReadFiles);
            if (!_directoryMoveToFiles.Exists)
            {
                _directoryMoveToFiles.Create();
            }
        }

        public void AddAllInDataBase(object fullNameFile)
        {
            IEnumerable<IObjectFromCsv> listCsvObjects = new List<IObjectFromCsv>();
            listCsvObjects = ReadFile(fullNameFile).ToList();


            FileInfo fileInfo = new FileInfo((string) fullNameFile);
            fileInfo.MoveTo(string.Format("{0}{1}", _directoryMoveToFiles.FullName, fileInfo.Name));

            OrderRepository orderRepository = new OrderRepository();
            List<OrderItem> listOrderItems = new List<OrderItem>();

            IEnumerable<IGrouping<int, IObjectFromCsv>> groupeById = listCsvObjects.GroupBy(x => x.OrderNumber);

            foreach (var item in groupeById)
            {
                foreach (var objectCsv in item)
                {
                    listOrderItems.Add(new OrderItem()
                    {
                        ItemId = objectCsv.ItemId,
                        Quantity = objectCsv.Quantity,
                        TotalCost = objectCsv.TotalCost
                    });
                }
                orderRepository.Add(new Order()
                {
                    OrderId = item.Key,
                    CustomerId = item.First().CustomerId,
                    Date = item.First().Date,
                    OrderItems = listOrderItems
                });
                listOrderItems.Clear();
            }
            orderRepository.Save();
        }

        protected IEnumerable<IObjectFromCsv> ReadFile(object fileFullName)
        {
            List<ObjectFromCsv> buf = new List<ObjectFromCsv>();

            using (TextFieldParser parser = new TextFieldParser((string) fileFullName))
            {
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                string[] strBuf = new string[6];
                parser.ReadFields();

                while (!parser.EndOfData)
                {
                    strBuf = parser.ReadFields();
                    buf.Add(new ObjectFromCsv()
                    {
                        Date = DateTime.Parse(strBuf[0]),
                        CustomerId = int.Parse(strBuf[1]),
                        ItemId = int.Parse(strBuf[2]),
                        TotalCost = int.Parse(strBuf[3]),
                        OrderNumber = int.Parse(strBuf[4]),
                        Quantity = int.Parse(strBuf[5])
                    });
                }

                return buf;
            }
        }
    }
}