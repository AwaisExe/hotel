using APPLICATION.Hotel.Models;
using INFRASTRUCTURE.Invariant;
using MediatR;

namespace APPLICATION.Hotel.Queries.JobGetAll
{
    public class HotelGetAllRequestDto : IRequest<EntityResponseListModel<HotelResponseDto>>
    {
        public string SearchText { get; set; }
    }
}
