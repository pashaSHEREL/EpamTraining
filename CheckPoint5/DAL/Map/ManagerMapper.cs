using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;

namespace DAL
{
    internal class ManagerMapper : Map<Manager, Models.Manager>
    {
        public override Models.Manager ConvertToObject(Manager entity)
        {
            if (entity != null)
            {
                return new Models.Manager()
                {
                    ManagerId = entity.manager_id,
                    FirstName = entity.first_name,
                    LastName = entity.last_name,
                    Address = entity.Adress,
                    PhoneNumber = entity.phone_number,
                    Email = entity.email,
                };
            }
            else
            {
                return null;
            }
        }

        public override Manager ConvertToEntity(Models.Manager obj)
        {
            if (obj != null)
            {
                return new Manager()
                {
                    manager_id = obj.ManagerId,
                    first_name = obj.FirstName,
                    last_name = obj.LastName,
                    Adress = obj.Address,
                    phone_number = obj.PhoneNumber,
                    email = obj.Email
                };
            }
            else
            {
                return null;
            }
        }
    }
}