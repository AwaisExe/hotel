using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Hotel.Queries.HotelGetById
{
    public class HotelGetByIdQueryValidator : BaseValidator<HotelGetByIdRequestDto>
    {
        public HotelGetByIdQueryValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
