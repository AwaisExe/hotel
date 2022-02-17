using APPLICATION.Booking.Commands.BookingCreate;
using APPLICATION.Booking.Models;
using INFRASTRUCTURE.Invariant;
using INFRASTRUCTURE.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Controllers
{
    [ApiVersion(Swagger.Versions.v1_0)]
    [Route(AspNet.Mvc.DefaultControllerTemplate)]
    [Produces(MediaTypeNames.Application.Json)]
    public class BookingController : ApiControllerBase
    {
        public BookingController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<BookingResponseDto>), 200)]
        public async Task<IActionResult> Create([FromBody] BookingCreateDto model)
        {
            var response = await _Mediator.Send(model);
            return Ok(response);
        }
    }
}
