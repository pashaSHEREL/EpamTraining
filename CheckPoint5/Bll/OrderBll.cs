using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;

namespace Bll
{
    public class OrderBll
    {
        private OrderRepository _orderRepository = new OrderRepository();

        public IEnumerable<OrderSummary> GetAll()
        {
            List<OrderSummary> listOrderSummaries = new List<OrderSummary>();

            foreach (var item in  _orderRepository.GetAll())
            {
                listOrderSummaries.Add(new OrderSummary()
                {
                    OrderId = item.OrderId,
                    Date = item.Date,
                    ManagerId = item.ManagerId,
                    Time = item.Time,
                    CustomerId = item.CustomerId,
                    TotalCost = item.OrderItems.Sum(x => x.TotalCost),
                    NumberOfItems = item.OrderItems.Sum(x => x.Quantity)
                });
            }

            return listOrderSummaries;
        }

        public void Update(Order order1, Order order2)
        {
            _orderRepository.Update(order1, order2);
        }

        public Order GetRecord(int id)
        {
            return _orderRepository.GetRecord(id);
        }

        public void Save()
        {
            _orderRepository.Save();
        }
    }
}