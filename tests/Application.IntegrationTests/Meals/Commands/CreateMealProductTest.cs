using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Meals.Commands.CreateMealProduct;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.Meals.Commands;

using static Testing;

public class CreateMealProductTest : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowNotFoundExceptionOnProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var product = new Product("test", 11, 11.0f, 12.0f, 13.0f);
        var meal = new Meal(userId, DateOnly.FromDateTime(DateTime.Now), "test");

        await AddAsync(product);
        await AddAsync(meal);

        var command = new CreateMealProductCommand
        {
            ProductId = Guid.NewGuid(),
            MealId = meal.Id
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldThrowNotFoundExceptionOnMeal()
    {
        var userId = await RunAsDefaultUserAsync();

        var product = new Product("test", 11, 11.0f, 12.0f, 13.0f);
        var meal = new Meal(userId, DateOnly.FromDateTime(DateTime.Now), "test");

        await AddAsync(product);
        await AddAsync(meal);

        var command = new CreateMealProductCommand
        {
            ProductId = product.Id,
            MealId = Guid.NewGuid()
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldAddProductToMeal()
    {
        var userId = await RunAsDefaultUserAsync();

        var product = new Product("test", 11, 11.0f, 12.0f, 13.0f);
        var meal = new Meal(userId, DateOnly.FromDateTime(DateTime.Now), "test");

        await AddAsync(product);
        await AddAsync(meal);

        var command = new CreateMealProductCommand
        {
            ProductId = product.Id,
            MealId = meal.Id
        };

        var mealProductId = await SendAsync(command);

        var mealProduct = await FindAsync<MealProduct>(mealProductId);

        mealProduct.Should().NotBeNull();
        mealProduct!.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        mealProduct.LastModifiedBy.Should().Be(userId);
        mealProduct.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
