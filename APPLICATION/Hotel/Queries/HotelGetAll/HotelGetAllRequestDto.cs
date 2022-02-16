
using APPLICATION.Hotel.Models;
using INFRASTRUCTURE.Invariant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Hotel.Queries.JobGetAll
{
    public class HotelGetAllRequestDto : IRequest<EntityResponseListModel<HotelResponseDto>>
    {

    }
}
