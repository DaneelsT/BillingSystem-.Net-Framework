using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Configs
{
    public class CityConfig : EntityTypeConfiguration<City>
    {
        public CityConfig()
        {
            ToTable("City");
            HasKey(c => c.CityId);

            Property(p => p.CityName)
                .HasColumnName("CityName")
                .HasColumnType("varchar")
                .HasMaxLength(90)
                .IsRequired();

            Property(p => p.ZipCode)
                .HasColumnName("ZipCode")
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();
        }
    }
}
