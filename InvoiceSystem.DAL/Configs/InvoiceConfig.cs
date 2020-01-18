using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Configs
{
    public class InvoiceConfig : EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfig()
        {
            ToTable("Invoice");
            HasKey(i => i.InvoiceId);

            Property(p => p.CustomerId)
                .HasColumnName("CustomerId")
                .HasColumnType("int")
                .IsRequired();

            Property(p => p.Date)
                .HasColumnName("Date")
                .HasColumnType("datetime2")
                .IsRequired();

            Property(p => p.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("bit")
                .IsRequired();

            Property(p => p.IsFinished)
                .HasColumnName("IsFinished")
                .HasColumnType("bit")
                .IsRequired();

            Property(p => p.Reason)
                .HasColumnName("Reason")
                .HasColumnType("varchar")
                .HasMaxLength(200);

            Property(p => p.UniqueCode)
                .HasColumnName("UniqueCode")
                .HasMaxLength(11)
                .IsRequired();
        }
    }
}
