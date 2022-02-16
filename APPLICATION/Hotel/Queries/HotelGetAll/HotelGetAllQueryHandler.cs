using APPLICATION.Hotel.Models;
using APPLICATION.Hotel.Queries.JobGetAll;
using AutoMapper;
using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Invariant;
using INFRASTRUCTURE.MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APPLICATION.Hotel.Queries.HotelGetAll
{
    public class HotelGetAllQueryHandler : RequestHandlerBase<HotelGetAllRequestDto, EntityResponseListModel<HotelResponseDto>>
    {
        public HotelGetAllQueryHandler(ILoggerFactory loggerFactory,
            IUnitOfWork unitOfWork, IMapper mapper) : base(loggerFactory, unitOfWork, mapper)
        {

        }

        protected async override Task<EntityResponseListModel<HotelResponseDto>> ProcessAsync(HotelGetAllRequestDto request,
            CancellationToken cancellationToken)
        {
            var hotels = await UnitOfWork.Set<DOMAIN.Entities.Hotel>()
                .AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();

            return new EntityResponseListModel<HotelResponseDto>
            {
                ReturnStatus = true,
                StatusCode = StatusCodes.Status200OK,
                Data = Mapper.Map<List<HotelResponseDto>>(hotels),
                Total = hotels.Count,
            };
        }
    }
}
