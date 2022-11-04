using Dietonator.Application.Meals.Commands.CreateMeal;
using Dietonator.Application.Meals.Commands.CreateMealProduct;
using Dietonator.Application.Meals.Commands.UpdateMealProduct;
using Dietonator.Application.Meals.Queries.GetMealsForDate;
using Microsoft.AspNetCore.Mvc;

namespace Dietonator.WebUI.Controllers;

public class MealController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<MealDto>> GetMeals([FromQuery]GetMealsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateMeal([FromBody] CreateMealCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("{mealId}/product")]
    public async Task<ActionResult<Guid>> CreateMealProduct(Guid mealId, [FromBody] CreateMealProductCommand command)
    {
        if (mealId != command.MealId)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }

    [HttpPut("{mealId}/product/{mealProductId}")]
    public async Task<ActionResult> UpdateMealProduct(Guid mealId, Guid mealProductId, [FromBody] UpdateMealProductCommand command)
    {
        if (mealId != command.MealId)
        {
            return BadRequest();
        }

        if (mealProductId != command.MealProductId)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}
