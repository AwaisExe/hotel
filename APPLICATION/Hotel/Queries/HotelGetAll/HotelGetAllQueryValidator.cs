using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Validator;

namespace APPLICATION.Hotel.Queries.JobGetAll
{
    public class HotelGetAllQueryValidator : BaseValidator<HotelGetAllRequestDto>
    {
        public HotelGetAllQueryValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
