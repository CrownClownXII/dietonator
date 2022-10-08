using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Meals.Commands.SetProductAmount;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.Meals.Commands;

using static Testing;

public class SetProductAmountTest : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowValidationException()
    {
        var command = new SetProductAmountCommand()
        {
            Amount = -1
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    public async Task ShouldThrowNotFoundException()
    {
        var command = new SetProductAmountCommand()
        {
            MealProductId = Guid.NewGuid(),
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldSetProductAmount()
    {
        var userId = await RunAsDefaultUserAsync();

        var product = new Product("test", 1, 1.1f, 1.1f, 1.1f);
        var mealProduct = new MealProduct(product, 100);
        var meal = new Meal(userId, DateOnly.FromDateTime(DateTime.Now), "A");

        meal.AddProduct(mealProduct);

        await AddAsync(meal);

        var command = new SetProductAmountCommand
        {
            MealProductId = mealProduct.Id,
            Amount = 10,
        };

        await SendAsync(command);

        var item = await FindAsync<MealProduct>(mealProduct.Id);

        item.Should().NotBeNull();
        item!.Amount.Should().Be(10);
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
