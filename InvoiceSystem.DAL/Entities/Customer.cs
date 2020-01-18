using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public bool IsDeleted { get; set; }
        public string PhoneNumber { get; set; }
        public string VatNumber { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Bus { get; set; }
        public string Email { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
