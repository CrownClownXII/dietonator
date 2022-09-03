using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.MealPlans.Commands.CreateMealPlan;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.MealPlans.Commands;

using static Testing;

public class AddMealPlanTest : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateMealPlan()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateMealPlanCommand
        {
            ForDate = DateOnly.FromDateTime(DateTime.Now),
        };

        var mealPlanId = await SendAsync(command);

        var item = await FindAsync<MealPlan>(mealPlanId);

        item.Should().NotBeNull();
        item!.ForDate.Should().Be(command.ForDate);
        item!.ForUser.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
