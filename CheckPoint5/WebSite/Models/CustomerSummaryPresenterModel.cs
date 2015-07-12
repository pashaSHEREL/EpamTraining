using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class CustomerSummaryPresenterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string OrderCount { get; set; }
        public string TotalSum { get; set; }
    }
}