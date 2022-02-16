using APPLICATION.Hotel.Models;
using AutoMapper;
using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Invariant;
using INFRASTRUCTURE.MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APPLICATION.Hotel.Queries.HotelGetById
{
    public class HotelGetByIdQueryHandler : RequestHandlerBase<HotelGetByIdRequestDto, EntityResponseModel<HotelResponseDto>>
    {
        public HotelGetByIdQueryHandler(ILoggerFactory loggerFactory,
            IUnitOfWork unitOfWork, IMapper mapper) : base(loggerFactory, unitOfWork, mapper)
        {

        }

        protected async override Task<EntityResponseModel<HotelResponseDto>> ProcessAsync(HotelGetByIdRequestDto request,
            CancellationToken cancellationToken)
        {
            var hotel = await UnitOfWork.Set<DOMAIN.Entities.Hotel>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return new EntityResponseModel<HotelResponseDto>
            {
                ReturnStatus = true,
                StatusCode = StatusCodes.Status200OK,
                Data = Mapper.Map<HotelResponseDto>(hotel)
            };
        }
    }
}
