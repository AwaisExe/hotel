using APPLICATION.Hotel.Models;
using APPLICATION.Hotel.Queries.HotelGetById;
using APPLICATION.Hotel.Queries.JobGetAll;
using INFRASTRUCTURE.Invariant;
using INFRASTRUCTURE.Swagger;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Controllers
{
    [ApiVersion(Swagger.Versions.v1_0)]
    [Route(AspNet.Mvc.DefaultControllerTemplate)]
    [Produces(MediaTypeNames.Application.Json)]
    public class HotelController : ApiControllerBase
    {

        [HttpGet]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseListModel<HotelResponseDto>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] HotelGetAllRequestDto model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }

        [HttpGet]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<HotelResponseDto>), 200)]
        public async Task<IActionResult> GetById([FromQuery] HotelGetByIdRequestDto model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }
    }
}
