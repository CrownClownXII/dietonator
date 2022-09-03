using Dietonator.Application.MealPlans.Commands.CreateMealPlan;
using Dietonator.Application.MealPlans.Queries.GetMealPlanDetails;
using Dietonator.Application.MealPlans.Queries.GetMealPlanListForUser;
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

    [HttpGet("forloggeduser")]
    public async Task<IEnumerable<MealPlanListDto>> GetList()
    {
        return await Mediator.Send(new GetMealPlanListForUserQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MealPlanDetailsDto>> GetDetails(Guid id)
    {
        return await Mediator.Send(new GetMealPlanDetailsQuery { MealPlanId = id });
    }
}
