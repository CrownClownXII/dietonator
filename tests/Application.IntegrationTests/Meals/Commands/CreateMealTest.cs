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
    public async Task ShouldCreateMeal()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateMealCommand()
        {
            Name = "Test",
            Type = MealTypeEnum.CalculableMeal
        };

        var productId = await SendAsync(command);

        var meal = await FindAsync<Meal>(productId);

        meal.Should().NotBeNull();
        meal!.Name.Should().Be(command.Name);
        meal.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        meal.LastModifiedBy.Should().Be(userId);
        meal.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
