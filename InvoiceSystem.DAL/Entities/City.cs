using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
