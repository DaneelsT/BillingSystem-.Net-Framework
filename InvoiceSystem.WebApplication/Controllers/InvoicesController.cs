using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InvoiceSystem.BLL.Services;
using InvoiceSystem.COMMON.DTOs;
using InvoiceSystem.DAL;
using InvoiceSystem.DAL.Entities;

namespace InvoiceSystem.WebApplication.Controllers
{
    [Authorize]

    public class InvoicesController : Controller
    {
        private InvoiceService _invoiceService;
        private CustomerService _customerService;
        private DetailLineService _detailLineService;
        public InvoicesController()
        {
            _invoiceService = new InvoiceService();
            _customerService = new CustomerService();
            _detailLineService = new DetailLineService();
        }

        // GET: Invoices
        
        public ActionResult Index()
        {
            List<InvoiceDTO> invoiceDTOs = _invoiceService.GetAllActiveInvoices();
            return View(invoiceDTOs);
        }
        public ActionResult FilteredIndex()
        {
            List<InvoiceDTO> invoiceDTOs = _invoiceService.GetAllInvoices();
            return View(invoiceDTOs);
        }
        public ActionResult FilteredFinishedIndex()
        {
            List<InvoiceDTO> invoiceDTOs = _invoiceService.GetAllFinishedInvoices();
            return View(invoiceDTOs);
        }
        public ActionResult FilteredDeletedIndex()
        {
            List<InvoiceDTO> invoiceDTOs = _invoiceService.GetAllDeletedInvoices();
            return View(invoiceDTOs);
        }
        public ActionResult CreatePdf(int? id)
        {
            InvoiceDTO invoiceDTO = _invoiceService.GetInvoiceById(id);
            string invoiceCode = invoiceDTO.UniqueCode;
            FileResult file = new FileContentResult(_invoiceService.CreateInvoice(id), "application/pdf");
            file.FileDownloadName = invoiceCode + ".pdf";
            return file;
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDTO invoice = _invoiceService.GetInvoiceById(id);
            invoice.CustomerDTO = _customerService.GetCustomerById(invoice.CustomerDTOId);
            List<DetailLineDTO> detailLineDTOs = _detailLineService.GetAllDetailLines().Where(d => d.InvoiceDTOId == invoice.InvoiceId).ToList();
            invoice.DetailLineDTOs = detailLineDTOs;
            decimal totalPrice = _invoiceService.GetTotalPrice(invoice);
            ViewBag.TotalPrice = totalPrice;
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.CustomerDTOId = new SelectList(_customerService.GetAllNotDeletedCustomers(), "CustomerId", "CompanyName");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,CustomerDTOId,Date,IsDeleted,IsFinished,Reason,UniqueCode")] InvoiceDTO invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.UniqueCode = _invoiceService.CreateUniqueCode(invoice.Date);
                _invoiceService.AddInvoice(invoice);

                return RedirectToAction("Index");
            }

            ViewBag.CustomerDTOId = new SelectList(_customerService.GetAllNotDeletedCustomers(), "CustomerId", "CompanyName", invoice.CustomerDTOId) ;
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDTO invoice = _invoiceService.GetInvoiceById(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerDTOId = new SelectList(_customerService.GetAllNotDeletedCustomers(), "CustomerId", "CompanyName", invoice.CustomerDTOId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,CustomerDTOId,Date,IsDeleted,IsFinished,Reason,UniqueCode")] InvoiceDTO invoice)
        {
            if (ModelState.IsValid)
            {
                _invoiceService.UpdateInvoice(invoice);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_customerService.GetAllNotDeletedCustomers(), "CustomerId", "CompanyName", invoice.CustomerDTOId);
            return View(invoice);
        }
        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDTO invoice = _invoiceService.GetInvoiceById(id);
            invoice.CustomerDTO = _customerService.GetCustomerById(invoice.CustomerDTOId);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceDTO invoice = _invoiceService.GetInvoiceById(id);
            invoice.CustomerDTO = _customerService.GetCustomerById(invoice.CustomerDTOId);
            if (_invoiceService.GetAllDetailLinesFromInvoice(id) == 0)
            {
                invoice.IsDeleted = true;
                _invoiceService.DeleteInvoice(invoice);
                return RedirectToAction("Index");
            }
            else
            {
                return View("DeleteWithReason",invoice);
            }
        }

        [HttpPost, ActionName("DeleteWithReason")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteWithReason([Bind(Include = "InvoiceId,CustomerDTOId,Date,IsDeleted,IsFinished,Reason,UniqueCode")] InvoiceDTO invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.CustomerDTOId = _invoiceService.GetInvoiceById(invoice.InvoiceId).CustomerDTOId;
                invoice.IsDeleted = true;
                
                _invoiceService.UpdateInvoice(invoice);
                return RedirectToAction("Index");
            }
            return View(invoice);
        }
    }
}
