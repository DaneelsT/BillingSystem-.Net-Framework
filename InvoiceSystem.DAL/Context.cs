using InvoiceSystem.DAL.Entities;
using InvoiceSystem.DAL.Configs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL
{
    public class Context: DbContext
    {
        public Context() : base("name=InvoiceSystemDatabase")
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DetailLine> DetailLines { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public void CreateModelUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new DetailLineConfig());
            modelBuilder.Configurations.Add(new InvoiceConfig());
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<InvoiceSystem.DAL.Entities.Role> Roles { get; set; }
    }
}
