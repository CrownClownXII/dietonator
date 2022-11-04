using Dietonator.Application.Common.Interfaces;
using Dietonator.Application.Products.Commands.CreateProduct;
using Dietonator.Application.Products.Queries;
using Dietonator.Application.Statistic.Queries;
using Dietonator.Application.Statistic.Queries.GetDailyUserStatistic;
using Dietonator.Application.Statistic.Queries.GetUserStatistic;
using Dietonator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.WebUI.Controllers;

public class StatisticController : ApiControllerBase
{
    [HttpGet("user")]
    public async Task<IEnumerable<UserStatisticDto>> Get([FromQuery] GetUserStatisticQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("details")]
    public async Task<DailyUserStatisticDto> Get([FromQuery] DateTime date)
    {
        return await Mediator.Send(new GetDailyUserStatisticQuery() { Date = date });
    }
}
