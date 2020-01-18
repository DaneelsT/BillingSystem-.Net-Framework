using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Repositories
{
    public class InvoiceRepository : IGenericRepository<Invoice>
    {
        private readonly Context _context;
        public InvoiceRepository(Context context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Invoice> GetAll()
        {
            return _context.Invoices.ToList();
        }

        public Invoice GetById(int? id)
        {
            return _context.Invoices.Find(id);
        }

        public void InsertEntity(Invoice entity)
        {
            _context.Invoices.Add(entity);
            _context.Customers.Attach(entity.Customer);
            _context.Entry(entity.Customer).State = EntityState.Unchanged;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateEntity(Invoice entity)
        {
            _context.Invoices.AddOrUpdate(entity);
            _context.Customers.Attach(entity.Customer);
            _context.Entry(entity.Customer).State = EntityState.Modified;
        }
        public void DeleteEntity(Invoice entity)
        {
            Invoice invoice = GetById(entity.InvoiceId);
            _context.Invoices.Remove(invoice);
            Save();
        }
    }
}
