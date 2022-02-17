using APPLICATION.Booking.Models;
using AutoMapper;
using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Invariant;
using INFRASTRUCTURE.MediatR;
using INFRASTRUCTURE.Persistence;
using Microsoft.AspNetCore.Http;

namespace APPLICATION.Booking.Commands.BookingCreate
{
    public class BookingCreateCommandHandler : RequestHandlerBase<BookingCreateDto, EntityResponseModel<BookingResponseDto>>
    {
        public BookingCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        protected async override Task<EntityResponseModel<BookingResponseDto>> ProcessAsync(BookingCreateDto request,
            CancellationToken cancellationToken)
        {
            var booking = Mapper.Map<DOMAIN.Entities.Booking>(request);
            UnitOfWork.Set<DOMAIN.Entities.Booking>().Add(booking);

            if (await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false) == 0)
                throw new DomainException(System.Net.HttpStatusCode.BadRequest, BookingValidationMessage.UnhandledError, false);

            return new EntityResponseModel<BookingResponseDto>
            {
                ReturnStatus = true,
                StatusCode = StatusCodes.Status200OK,
                Data = Mapper.Map<BookingResponseDto>(booking)
            };
        }
    }
}
