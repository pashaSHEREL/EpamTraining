using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;

namespace Bll
{
    public class CustomerBll
    {
        private CustomerRepository _customerRepository = new CustomerRepository();
        private OrderRepository _orderRepository = new OrderRepository();

        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public IEnumerable<CustomerSummaryPresenterModel> GetAllSummary()
        {
            List<CustomerSummaryPresenterModel> customerSummary = new List<CustomerSummaryPresenterModel>();
            foreach (var customer in _customerRepository.GetAll())
            {
                int? totalSum = 0;

                foreach (var order in customer.Orders)
                {
                    var orderObj = _orderRepository.GetRecord(order.OrderId);
                    foreach (var orderItem in orderObj.OrderItems)
                    {
                        totalSum += orderItem.TotalCost;
                    }
                }


                customerSummary.Add(new CustomerSummaryPresenterModel()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.FirstName,
                    Address = customer.Address,
                    OrderCount = customer.Orders.Count().ToString(),
                    TotalSum = totalSum.ToString()
                });
            }
            return customerSummary;
        }

        public void Add(Customer customer)
        {
            _customerRepository.Add(customer);
        }

        public void Delete(Customer customer)
        {
            _customerRepository.Delete(customer);
        }

        public void Update(Customer customer1, Customer customer2)
        {
            _customerRepository.Update(customer1, customer2);
        }

        public Customer GetRecord(int id)
        {
            return _customerRepository.GetRecord(id);
        }

        public void Save()
        {
            _customerRepository.Save();
        }
    }
}