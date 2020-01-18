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

    public class DetailLinesController : Controller
    {
        private DetailLineService _detailLineService;
        private InvoiceService _invoiceService;

        public DetailLinesController()
        {
            _detailLineService = new DetailLineService();
            _invoiceService = new InvoiceService();
        }

        // GET: DetailLines
        public ActionResult Index()
        {
            return View(_detailLineService.GetAllDetailLines());
        }

        // GET: DetailLines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailLineDTO detailLine = _detailLineService.GetDetailLineById(id);
            if (detailLine == null)
            {
                return HttpNotFound();
            }
            return View(detailLine);
        }

        // GET: DetailLines/Create
        public ActionResult Create(int? id)
        {
            InvoiceDTO invoiceDTO = _invoiceService.GetInvoiceById(id);
            IEnumerable<InvoiceDTO> currentInvoice = _invoiceService.GetAllInvoices().Where(i => i.InvoiceId == invoiceDTO.InvoiceId);
            ViewBag.InvoiceDTOId = new SelectList(currentInvoice, "InvoiceId", "UniqueCode");
            return View();
        }

        // POST: DetailLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DetailLineId,Item,UnitPrice,Discount,Amount,Vat,InvoiceDTOId")] DetailLineDTO detailLine)
        {
            if (ModelState.IsValid)
            {
                _detailLineService.AddDetailLine(detailLine);
                return RedirectToAction("Details","Invoices", new {id = detailLine.InvoiceDTOId });
            }
            IEnumerable<InvoiceDTO> currentInvoice = _invoiceService.GetAllInvoices().Where(i => i.InvoiceId == detailLine.InvoiceDTOId);
            ViewBag.InvoiceDTOId = new SelectList(currentInvoice, "InvoiceId", "UniqueCode", detailLine.InvoiceDTOId);
            return View(detailLine);
        }

        // GET: DetailLines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailLineDTO detailLine = _detailLineService.GetDetailLineById(id);
            if (detailLine == null)
            {
                return HttpNotFound();
            }
            IEnumerable<InvoiceDTO> currentInvoice = _invoiceService.GetAllInvoices().Where(i => i.InvoiceId == detailLine.InvoiceDTOId);
            ViewBag.InvoiceDTOId = new SelectList(currentInvoice, "InvoiceId", "UniqueCode", detailLine.InvoiceDTOId);
            return View(detailLine);
        }

        // POST: DetailLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DetailLineId,Item,UnitPrice,Discount,Amount,Vat,InvoiceDTOId")] DetailLineDTO detailLine)
        {
            if (ModelState.IsValid)
            {
                _detailLineService.UpdateDetailLine(detailLine);
                return RedirectToAction("Index");
            }
            IEnumerable<InvoiceDTO> currentInvoice = _invoiceService.GetAllInvoices().Where(i => i.InvoiceId == detailLine.InvoiceDTOId);
            ViewBag.InvoiceDTOId = new SelectList(currentInvoice, "InvoiceId", "UniqueCode", detailLine.InvoiceDTOId);
            return View(detailLine);
        }

        // GET: DetailLines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailLineDTO detailLine = _detailLineService.GetDetailLineById(id);
            if (detailLine == null)
            {
                return HttpNotFound();
            }
            return View(detailLine);
        }

        // POST: DetailLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailLineDTO detailLine = _detailLineService.GetDetailLineById(id);
            _detailLineService.DeleteDetailLine(detailLine);
            return RedirectToAction("Details", "Invoices", new { id = detailLine.InvoiceDTOId });
        }
    }
}
