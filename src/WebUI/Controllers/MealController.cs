using Dietonator.Application.Meals.Commands.CreateMeal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dietonator.WebUI.Controllers;

public class MealController : ApiControllerBase
{
    [HttpPost]
    public async Task<Guid> Post([FromBody] CreateMealCommand command)
    {
        return await Mediator.Send(command);
    }
}
