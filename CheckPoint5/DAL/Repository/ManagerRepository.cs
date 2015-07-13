using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;
using DAL;

namespace DAL
{
    public class ManagerRepository : Repository<Manager, Models.Manager, OrdersEntities>
    {
        public ManagerRepository()
        {
            _map = new ManagerMapper();
        }

        public override IEnumerable<Models.Manager> GetAll()
        {
            var managers = _context.Managers.Include("Orders");
            List<Models.Manager> listManagers = new List<Models.Manager>();

            foreach (var item in managers)
            {
                listManagers.Add(this.GetRecord(item.manager_id));
            }
            return listManagers;
        }

        public override void Update(Models.Manager obj1, Models.Manager obj2)
        {
            var l = _context.Managers.FirstOrDefault(x => x.manager_id == obj1.ManagerId);

            if (l != null)
            {
                l.first_name = obj2.FirstName;
                l.last_name = obj2.LastName;
                l.Adress = obj2.Address;
                l.phone_number = obj2.PhoneNumber;
                l.email = obj2.Email;
            }
        }

        public override void Delete(Models.Manager obj)
        {
            var l = _context.Customers.FirstOrDefault(x => x.customer_id == obj.ManagerId);
            _context.DeleteObject(l);
        }

        public override Models.Manager GetRecord(int id)
        {
            OrderMapper orderMapper = new OrderMapper();
            var record = _context.Managers.Include("Orders").FirstOrDefault(x => x.manager_id == id);
            Models.Manager manager = _map.ConvertToObject(record);

            List<Models.Order> orders = record.Orders.Select(item => orderMapper.ConvertToObject(item)).ToList();
            manager.Orders = orders;

            return manager;
        }
    }
}