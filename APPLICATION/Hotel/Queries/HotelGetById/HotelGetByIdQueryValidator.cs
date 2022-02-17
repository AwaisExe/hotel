using FluentValidation;
using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Validator;

namespace APPLICATION.Hotel.Queries.HotelGetById
{
    public class HotelGetByIdQueryValidator : BaseValidator<HotelGetByIdRequestDto>
    {
        public HotelGetByIdQueryValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(x => x.Id).NotEmpty().OverridePropertyName("Id").WithMessage("Id is required");
            RuleFor(p => p.Id).GreaterThan(0).OverridePropertyName("Id2").WithMessage("Id cannot be 0");
        }
    }
}
