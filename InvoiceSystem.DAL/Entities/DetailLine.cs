using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Entities
{
    public class DetailLine
    {
        public int DetailLineId { get; set; }
        public string Item { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Amount { get; set; }
        public decimal Vat { get; set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}
