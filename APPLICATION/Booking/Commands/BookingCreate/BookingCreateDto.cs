using APPLICATION.Booking.Models;
using INFRASTRUCTURE.Invariant;
using MediatR;

namespace APPLICATION.Booking.Commands.BookingCreate
{
    public class BookingCreateDto : BookingDto, IRequest<EntityResponseModel<BookingResponseDto>>
    {

    }
}
