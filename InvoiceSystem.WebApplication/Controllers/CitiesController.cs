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
    public class CitiesController : Controller
    {
        private CityService _cityService;
        public CitiesController()
        {
            _cityService = new CityService();
        }

        // GET: Cities
        public ActionResult Index()
        {
            return View(_cityService.GetAllCities());
        }

        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityDTO city = _cityService.GetCityById(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityId,ZipCode,CityName")] CityDTO city)
        {
            if (ModelState.IsValid)
            {
                _cityService.AddCity(city);

                return RedirectToAction("Create", "Customers");
            }

            return View(city);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityDTO city = _cityService.GetCityById(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityId,ZipCode,CityName")] CityDTO city)
        {
            if (ModelState.IsValid)
            {
                _cityService.UpdateCity(city);
                return RedirectToAction("Index");
            }
            return View(city);
        }
    }
}
