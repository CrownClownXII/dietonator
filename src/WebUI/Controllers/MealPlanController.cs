using Dietonator.Application.MealPlans.Commands.CreateMealPlan;
using Dietonator.Application.MealPlans.Queries.GetMealsList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dietonator.WebUI.Controllers;

public class MealPlanController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Post([FromBody] CreateMealPlanCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MealPlanDetailsDto>> Get(Guid id)
    {
        return await Mediator.Send(new GetMealPlanDetailsQuery { MealPlanId = id});
    }
}
