using AutoMapper;
using InvoiceSystem.COMMON.DTOs;
using InvoiceSystem.DAL.Entities;
using InvoiceSystem.DAL.Repositories;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.BLL.Services
{
    public partial class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly DetailLineService _detailLineService;
        private readonly CustomerService _customerService;
        private readonly CityService _cityService;
        private readonly AutoMapping _mapper;
        public InvoiceService()
        {
            _invoiceRepository = new InvoiceRepository(new DAL.Context());
            _customerRepository = new CustomerRepository(new DAL.Context());
            _detailLineService = new DetailLineService();
            _customerService = new CustomerService();
            _cityService = new CityService();
            _mapper = new AutoMapping();
        }
        public List<InvoiceDTO> GetAllInvoices()
        {
            List<InvoiceDTO> invoiceDTOs = new List<InvoiceDTO>();
            List<Invoice> invoices = _invoiceRepository.GetAll();
            int counter = 0;
            foreach (var item in invoices)
            {
                invoiceDTOs.Add(_mapper.Mapper().Map<InvoiceDTO>(item));
                invoiceDTOs[counter].CustomerDTO = _mapper.Mapper().Map<CustomerDTO>(_customerRepository.GetById(invoiceDTOs[counter].CustomerDTOId));
                counter++;

            }
            return invoiceDTOs;
        }

        public InvoiceDTO GetInvoiceById(int? id)
        {
            return _mapper.Mapper().Map<Invoice, InvoiceDTO>(_invoiceRepository.GetById(id));
        }
        public void AddInvoice(InvoiceDTO invoiceDTO)
        {
            Invoice invoice = _mapper.Mapper().Map<Invoice>(invoiceDTO);
            invoice.IsDeleted = false;
            invoice.IsFinished = false;

            invoice.CustomerId = invoiceDTO.CustomerDTOId;
            invoice.Customer = _customerRepository.GetById(invoice.CustomerId);

            _invoiceRepository.InsertEntity(invoice);
            _invoiceRepository.Save();
        }
        public void UpdateInvoice(InvoiceDTO invoiceDTO)
        {
            Invoice invoice = _mapper.Mapper().Map<Invoice>(invoiceDTO);

            invoice.CustomerId = invoiceDTO.CustomerDTOId;
            invoice.Customer = _customerRepository.GetById(invoice.CustomerId);

            _invoiceRepository.UpdateEntity(invoice);
            _invoiceRepository.Save();
        }

        public void DeleteInvoice(InvoiceDTO invoiceDTO)
        {
            Invoice invoice = _mapper.Mapper().Map<Invoice>(invoiceDTO);

            _invoiceRepository.DeleteEntity(invoice);
        }

        public string CreateUniqueCode(DateTime date)
        {
            string year = date.ToString("yyyy");
            string month = date.ToString("MM");
            int monthlyInvoice = GetLatestUniqueCode(date);
            return year + month + "-" + monthlyInvoice.ToString("D" + 4);
        }

        public int GetLatestUniqueCode(DateTime date)
        {
            List<InvoiceDTO> invoiceDTOs = GetAllInvoices();
            List<InvoiceDTO> monthlyInvoices = invoiceDTOs.Where(m => m.Date.Month == date.Month).ToList();
            int totalMonthlyInvoices = monthlyInvoices.Count;
            
            return totalMonthlyInvoices;
            
        }
        public int GetAllDetailLinesFromInvoice(int id)
        {
            int counter = 0;
            List<DetailLineDTO> detailLineDTOs = _detailLineService.GetAllDetailLines();
            foreach (var item in detailLineDTOs)
            {
                if (item.InvoiceDTOId == id)
                {
                    counter++;
                }
            }
            return counter;
        }
        public decimal GetTotalPrice(InvoiceDTO invoiceDTO)
        {
            decimal totalPrice = 0;
            List<DetailLineDTO> detailLineDTOs = _detailLineService.GetAllDetailLines().Where(d => d.InvoiceDTOId == invoiceDTO.InvoiceId).ToList();

            foreach (var item in detailLineDTOs)
            {
                totalPrice += item.TotalPriceWithDiscountAndVat;
            }
            return totalPrice;
        }
        public List<InvoiceDTO> GetAllActiveInvoices()
        {
            List<InvoiceDTO> invoiceDTOs = new List<InvoiceDTO>();
            List<Invoice> invoices = _invoiceRepository.GetAll().Where(i => i.IsDeleted == false).ToList();
            int counter = 0;
            foreach (var item in invoices)
            {
                invoiceDTOs.Add(_mapper.Mapper().Map<InvoiceDTO>(item));
                invoiceDTOs[counter].CustomerDTO = _mapper.Mapper().Map<CustomerDTO>(_customerRepository.GetById(invoiceDTOs[counter].CustomerDTOId));
                counter++;

            }
            return invoiceDTOs;
        }
        public List<InvoiceDTO> GetAllFinishedInvoices()
        {
            List<InvoiceDTO> invoiceDTOs = new List<InvoiceDTO>();
            List<Invoice> invoices = _invoiceRepository.GetAll().Where(i => i.IsFinished == true).ToList();
            int counter = 0;
            foreach (var item in invoices)
            {
                invoiceDTOs.Add(_mapper.Mapper().Map<InvoiceDTO>(item));
                invoiceDTOs[counter].CustomerDTO = _mapper.Mapper().Map<CustomerDTO>(_customerRepository.GetById(invoiceDTOs[counter].CustomerDTOId));
                counter++;

            }
            return invoiceDTOs;
        }
        public List<InvoiceDTO> GetAllDeletedInvoices()
        {
            List<InvoiceDTO> invoiceDTOs = new List<InvoiceDTO>();
            List<Invoice> invoices = _invoiceRepository.GetAll().Where(i => i.IsDeleted == true).ToList();
            int counter = 0;
            foreach (var item in invoices)
            {
                invoiceDTOs.Add(_mapper.Mapper().Map<InvoiceDTO>(item));
                invoiceDTOs[counter].CustomerDTO = _mapper.Mapper().Map<CustomerDTO>(_customerRepository.GetById(invoiceDTOs[counter].CustomerDTOId));
                counter++;

            }
            return invoiceDTOs;
        }

        public byte[] CreateInvoice(int? invoiceId)
        {
            InvoiceDTO invoiceDTO = GetInvoiceById(invoiceId);

            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument document = converter.ConvertHtmlString(GetInvoiceTemplate(invoiceDTO));
            byte[] doc = document.Save();
            document.Close();

            return doc;
        }
    }
}
