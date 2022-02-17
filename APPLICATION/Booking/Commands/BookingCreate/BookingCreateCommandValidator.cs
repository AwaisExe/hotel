using FluentValidation;
using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Validator;

namespace APPLICATION.Booking.Commands.BookingCreate
{
    public class BookingCreateCommandValidator : BaseValidator<BookingCreateDto>
    {
        public BookingCreateCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(x => x.HotelId).NotEmpty().OverridePropertyName("HotelId").WithMessage("HotelId is required");
            RuleFor(p => p.HotelId).GreaterThan(0).OverridePropertyName("HotelId2").WithMessage("HotelId cannot be 0");
        }
    }
}
