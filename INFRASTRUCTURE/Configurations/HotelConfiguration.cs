using DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRASTRUCTURE.Configurations
{
    public class HotelConfiguration : BaseConfiguration<Hotel>
    {
        public HotelConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {
            _builder.HasKey(t => t.Id);
            _builder.Property(t => t.Id).ValueGeneratedOnAdd();
            _builder.Property(p => p.Name).HasColumnType("nvarchar(50)");
            _builder.Property(p => p.Address).HasColumnType("nvarchar(1000)");
            _builder.Property(p => p.Lattitude).HasColumnType("varchar(100)");
            _builder.Property(p => p.Longitude).HasColumnType("varchar(100)");

            _builder.Property(p => p.CreatedOn).IsRequired(true);
            _builder.Property(p => p.CreatedBy).HasColumnType("nvarchar(50)");
            _builder.Property(p => p.UpdatedOn)
                .IsRequired(false);
            _builder.Property(p => p.UpdatedBy)
                .HasColumnType("nvarchar(50)");

            _builder.ToTable("Hotel", "dbo");
        }
    }
}