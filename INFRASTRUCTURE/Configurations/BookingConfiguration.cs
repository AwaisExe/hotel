using DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRASTRUCTURE.Configurations
{
    public class BookingConfiguration : BaseConfiguration<Booking>
    {
        public BookingConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {
            _builder.HasKey(t => t.Id);
            _builder.Property(t => t.Id).ValueGeneratedOnAdd();
            _builder.Property(p => p.FirstName).HasColumnType("nvarchar(100)");
            _builder.Property(p => p.LastName).HasColumnType("nvarchar(100)");
            _builder.Property(p => p.EmailAddress).HasColumnType("varchar(100)");
            _builder.Property(p => p.MobileNo).HasColumnType("varchar(25)");
            _builder.Property(p => p.PassportNo).HasColumnType("varchar(50)");

            _builder.Property(p => p.CreatedOn).IsRequired(true);
            _builder.Property(p => p.CreatedBy).HasColumnType("nvarchar(50)");
            _builder.Property(p => p.UpdatedOn)
                .IsRequired(false);
            _builder.Property(p => p.UpdatedBy)
                .HasColumnType("nvarchar(50)");

            _builder.ToTable("Booking", "dbo");

            _builder.HasOne(x => x.Hotel)
             .WithMany(x => x.Bookings)
             .HasForeignKey(x => x.HotelId)
             .HasConstraintName("FK_Bookings_Hotel_HotelId")
             .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}