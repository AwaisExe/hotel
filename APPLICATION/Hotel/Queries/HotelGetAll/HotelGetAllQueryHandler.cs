using APPLICATION.Hotel.Models;
using APPLICATION.Hotel.Queries.JobGetAll;
using AutoMapper;
using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Invariant;
using INFRASTRUCTURE.MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace APPLICATION.Hotel.Queries.HotelGetAll
{
    public class HotelGetAllQueryHandler : RequestHandlerBase<HotelGetAllRequestDto, EntityResponseListModel<HotelResponseDto>>
    {
        public HotelGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        protected async override Task<EntityResponseListModel<HotelResponseDto>> ProcessAsync(HotelGetAllRequestDto request,
            CancellationToken cancellationToken)
        {

            var hotels = UnitOfWork.Set<DOMAIN.Entities.Hotel>()
                .Where(x => EF.Functions.Like(x.Name + x.Address, "%" + request.SearchText + "%")).ToList();

            return new EntityResponseListModel<HotelResponseDto>
            {
                ReturnStatus = true,
                StatusCode = StatusCodes.Status200OK,
                Data = Mapper.Map<List<HotelResponseDto>>(hotels),
                Total = hotels.Count(),
            };
        }
    }
}
