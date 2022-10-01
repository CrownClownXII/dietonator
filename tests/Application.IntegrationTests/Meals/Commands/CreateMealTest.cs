using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Meals.Commands.CreateMeal;
using Dietonator.Domain.Entities;
using Dietonator.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.Meals.Commands;

using static Testing;

public class CreateMealTest : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowValidationException()
    {
        var command = new CreateMealCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldThrowNotFoundException()
    {
        var command = new CreateMealCommand()
        {
            Name = "Test",
            Type = MealTypeEnum.CalculableMeal,
            MealPlanId = Guid.NewGuid()
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldCreateMeal()
    {
        var userId = await RunAsDefaultUserAsync();

        var mealPlan = new MealPlan(DateOnly.FromDateTime(DateTime.Now), userId);

        await AddAsync(mealPlan);

        var command = new CreateMealCommand()
        {
            Name = "Test",
            Type = MealTypeEnum.CalculableMeal,
            MealPlanId = mealPlan.Id
        };

        var mealId = await SendAsync(command);

        var meal = await FindAsync<Meal>(mealId);

        meal.Should().NotBeNull();
        meal!.Name.Should().Be(command.Name);
        meal.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        meal.LastModifiedBy.Should().Be(userId);
        meal.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
