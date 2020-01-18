using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Configs
{
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            ToTable("Customer");
            HasKey(c => c.CustomerId);

            Property(p => p.CityId)
                .HasColumnName("CityId")
                .HasColumnType("int");

            Property(p => p.CompanyName)
                .HasColumnName("CompanyName")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            Property(p => p.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            Property(p => p.HouseNumber)
                .HasColumnName("HouseNumber")
                .HasColumnType("varchar")
                .HasMaxLength(200);

            Property(p => p.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("bit")
                .IsRequired();

            Property(p => p.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(p => p.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            Property(p => p.Bus)
                .HasColumnName("Bus")
                .HasColumnType("varchar")
                .HasMaxLength(200);

            Property(p => p.VatNumber)
                .HasColumnName("VatNumber")
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();
        }
    }
}
