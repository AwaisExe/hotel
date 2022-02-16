using APPLICATION.Hotel.Models;
using INFRASTRUCTURE.Invariant;
using MediatR;

namespace APPLICATION.Hotel.Queries.HotelGetById
{
    public class HotelGetByIdRequestDto : IRequest<EntityResponseModel<HotelResponseDto>>
    {
        public int Id { get; set; }
    }
}
