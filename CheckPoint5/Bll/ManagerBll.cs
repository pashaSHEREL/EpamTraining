using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;

namespace Bll
{
    public class ManagerBll
    {
        private ManagerRepository _managerRepository = new ManagerRepository();
       

        public IEnumerable<Manager> GetAll()
        {
            return _managerRepository.GetAll();
        }

        public void Add(Manager manager)
        {
            _managerRepository.Add(manager);
        }

        public void Delete(Manager manager)
        {
            _managerRepository.Delete(manager);
        }

        public void Update(Manager manager1, Manager manager2)
        {
            _managerRepository.Update(manager1, manager2);
        }

        public Manager GetRecord(int id)
        {
            return _managerRepository.GetRecord(id);
        }

        public void Save()
        {
            _managerRepository.Save();
        }
    }
}