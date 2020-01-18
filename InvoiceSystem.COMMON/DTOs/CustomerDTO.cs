using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.COMMON.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public CityDTO CityDTO { get; set; }
        public int CityDTOId { get; set; }
        public bool IsDeleted { get; set; }
        public string PhoneNumber { get; set; }
        public string VatNumber { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Bus { get; set; }
        public string Email { get; set; }
        public List<InvoiceDTO> InvoiceDTOs { get; set; }
    }
}
