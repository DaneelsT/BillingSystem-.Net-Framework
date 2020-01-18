using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.COMMON.DTOs
{
   public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
        public int CustomerDTOId { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFinished { get; set; }
        public string Reason { get; set; }
        public string UniqueCode { get; set; }
        public List<DetailLineDTO> DetailLineDTOs { get; set; }
    }
}
