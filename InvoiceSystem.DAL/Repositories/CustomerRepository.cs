using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace InvoiceSystem.DAL.Repositories
{
   public class CustomerRepository : IGenericRepository<Customer>
    {
        private readonly Context _context;
        public CustomerRepository(Context context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Customer> GetAll()
        {
            // Maak gebruik van één stored procedure call in je data laag.
            return _context.Database.SqlQuery<Customer>("SelectAllCustomers").ToList();
        }

        public Customer GetById(int? id)
        {
            return _context.Customers.Find(id);
        }

        public void InsertEntity(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.Cities.Attach(entity.City);
            _context.Entry(entity.City).State = EntityState.Unchanged;
        }

        public void UpdateEntity(Customer entity)
        {
            _context.Customers.AddOrUpdate(entity);
            _context.Cities.Attach(entity.City);
            _context.Entry(entity.City).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
