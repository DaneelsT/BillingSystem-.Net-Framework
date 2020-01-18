using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Repositories
{
    public class DetailLineRepository : IGenericRepository<DetailLine>
    {
        private readonly Context _context;
        public DetailLineRepository(Context context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public List<DetailLine> GetAll()
        {
            return _context.DetailLines.ToList();
        }

        public DetailLine GetById(int? id)
        {
            return _context.DetailLines.Find(id);
        }

        public void InsertEntity(DetailLine entity)
        {
            _context.DetailLines.Add(entity);
            _context.Invoices.Attach(entity.Invoice);
            _context.Entry(entity.Invoice).State = EntityState.Unchanged;
        }

        public void UpdateEntity(DetailLine entity)
        {
            _context.DetailLines.AddOrUpdate(entity);
            _context.Invoices.Attach(entity.Invoice);
            _context.Entry(entity.Invoice).State = EntityState.Modified;
        }
        public void DeleteEntity(DetailLine entity)
        {
            DetailLine detailLine = GetById(entity.DetailLineId);
            _context.DetailLines.Remove(detailLine);
            _context.SaveChanges();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
