using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Repositories
{
    public class CityRepository : IGenericRepository<City>
    {
        private readonly Context _context;

        public CityRepository(Context context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<City> GetAll()
        {
            // Maak gebruik van één zelf uitgewerkte inline query in je data laag.
            return _context.Cities.SqlQuery("Select * from dbo.Cities").ToList();
        }

        public City GetById(int? id)
        {
            return _context.Cities.Find(id);
        }

        public void InsertEntity(City entity)
        {
            _context.Cities.Add(entity);
        }

        public void UpdateEntity(City entity)
        {
            _context.Cities.AddOrUpdate(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
