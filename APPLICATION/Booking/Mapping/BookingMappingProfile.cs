using APPLICATION.Booking.Commands.BookingCreate;
using APPLICATION.Booking.Models;
using AutoMapper;

namespace APPLICATION.Booking.Mapping
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<BookingCreateDto, DOMAIN.Entities.Booking>();
            CreateMap<DOMAIN.Entities.Booking, BookingResponseDto>();
        }
    }
}
