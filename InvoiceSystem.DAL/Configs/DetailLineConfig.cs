using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Configs
{
    public class DetailLineConfig : EntityTypeConfiguration<DetailLine>
    {
        public DetailLineConfig()
        {
            ToTable("DetailLine");
            HasKey(d => d.DetailLineId);

            Property(p => p.Amount)
                .HasColumnName("Amount")
                .HasColumnType("int")
                .IsRequired();

            Property(p => p.Discount)
                .HasColumnName("Discount")
                .HasColumnType("decimal")
                .IsRequired();

            Property(p => p.InvoiceId)
                .HasColumnName("InvoiceId")
                .HasColumnType("int")
                .IsRequired();

            Property(p => p.Item)
                .HasColumnName("Item")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            Property(p => p.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal")
                .IsRequired();

            Property(p => p.Vat)
                .HasColumnName("VAT")
                .HasColumnType("decimal")
                .IsRequired();
        }
    }
}
