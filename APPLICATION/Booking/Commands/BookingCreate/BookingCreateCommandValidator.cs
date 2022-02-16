using FluentValidation;
using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Booking.Commands.BookingCreate
{
    public class BookingCreateCommandValidator : BaseValidator<BookingCreateDto>
    {
        public BookingCreateCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
        }
    }
}
