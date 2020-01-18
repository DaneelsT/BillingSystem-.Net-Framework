using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFinished { get; set; }
        public string Reason { get; set; }
        public string UniqueCode { get; set; }
        public List<DetailLine> DetailLines { get; set; }
    }
}
