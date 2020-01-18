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

namespace InvoiceSystem.WebApplication.Controllers
{
    [Authorize]

    public class CustomersController : Controller
    {
        private CustomerService _customerService;
        private CityService _cityService;

        public CustomersController()
        {
            _customerService = new CustomerService();
            _cityService = new CityService();
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View(_customerService.GetAllCustomers());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDTO customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CityDTOId = new SelectList(_cityService.GetAllCities(), "CityId", "CityName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CompanyName,CityDTOId,IsDeleted,PhoneNumber,VatNumber,Street,HouseNumber,Bus,Email")] CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.AddCustomer(customer);
                return RedirectToAction("Index");
            }

            ViewBag.CityDTOId = new SelectList(_cityService.GetAllCities(), "CityId", "CityId", customer.CityDTOId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDTO customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityDTOId = new SelectList(_cityService.GetAllCities(), "CityId", "ZipCode", customer.CityDTOId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CompanyName,CityDTOId,IsDeleted,PhoneNumber,VatNumber,Street,HouseNumber,Bus,Email")] CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }

            ViewBag.CityDTOId = new SelectList(_cityService.GetAllCities(), "CityId", "ZipCode", customer.CityDTOId);
            return View(customer);
        }
    }
}
