using Dietonator.Application.Meals.Commands.AddProductToMeal;
using Dietonator.Application.Meals.Commands.CreateMeal;
using Dietonator.Application.Meals.Commands.SetProductAmount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dietonator.WebUI.Controllers;

public class MealController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Post([FromBody] CreateMealCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("product")]
    public async Task<ActionResult<Guid>> AddToMeal([FromBody] AddProductToMealCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("product/{mealProductId}")]
    public async Task<ActionResult> SetAmountToMealProduct(Guid mealProductId, SetProductAmountCommand command)
    {
        if (mealProductId != command.MealProductId)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}
