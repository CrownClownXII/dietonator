using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Meals.Commands.AddProductToMeal;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.Meals.Commands;

using static Testing;

public class AddProductToMealTest : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowNotFoundExceptionOnProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var productId = await AddAsync(new Product("test", 11, 11.0f, 12.0f, 13.0f));
        var mealId = await AddAsync(new Meal("test"));

        var command = new AddProductToMealCommand
        {
            ProductId = Guid.NewGuid(),
            MealId = mealId
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldThrowNotFoundExceptionOnMeal()
    {
        var userId = await RunAsDefaultUserAsync();

        var productId = await AddAsync(new Product("test", 11, 11.0f, 12.0f, 13.0f));
        var mealId = await AddAsync(new Meal("test"));

        var command = new AddProductToMealCommand
        {
            ProductId = productId,
            MealId = Guid.NewGuid()
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldAddProductToMeal()
    {
        var userId = await RunAsDefaultUserAsync();

        var productId = await AddAsync(new Product("test", 11, 11.0f, 12.0f, 13.0f));
        var mealId = await AddAsync(new Meal("test"));

        var command = new AddProductToMealCommand
        {
            ProductId = productId,
            MealId = mealId
        };

        var mealProductId = await SendAsync(command);

        var mealProduct = await FindAsync<MealProduct>(mealProductId);

        mealProduct.Should().NotBeNull();
        mealProduct!.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        mealProduct.LastModifiedBy.Should().Be(userId);
        mealProduct.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
