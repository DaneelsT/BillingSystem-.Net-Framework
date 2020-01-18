using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.COMMON.DTOs
{
   public class DetailLineDTO
    {
        public int DetailLineId { get; set; }
        public string Item { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Amount { get; set; }
        public decimal Vat { get; set; }
        public InvoiceDTO InvoiceDTO { get; set; }
        public int InvoiceDTOId { get; set; }
        public decimal TotalWithDiscount { get; set; }
        public decimal TotalPriceWithDiscountAndVat { get; set; }
    }
}
