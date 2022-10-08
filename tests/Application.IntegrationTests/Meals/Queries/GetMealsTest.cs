using Dietonator.Application.Meals.Queries.GetMealsForDate;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.MealPlans.Queries;

using static Testing;

public class GetMealsTest : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnAllUserMealPlans()
    {
        var userId = await RunAsDefaultUserAsync();

        var today = DateOnly.FromDateTime(DateTime.Now);
        var yesterday = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        var meal1 = new Meal(userId, today, "test1");
        var meal2 = new Meal(Guid.NewGuid(), today, "test2");
        var meal3 = new Meal(userId, today,"test3");
        var meal4 = new Meal(userId, yesterday, "test4");
        var meal5 = new Meal(userId, today, "test5");

        await AddAsync(meal1);
        await AddAsync(meal2);
        await AddAsync(meal3);
        await AddAsync(meal4);
        await AddAsync(meal5);

        var query = new GetMealsQuery() 
        {
            ForDate = null
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result?.Should().HaveCount(4);
    }

    [Test]
    public async Task ShouldReturnMealPlanForDate()
    {
        var userId = await RunAsDefaultUserAsync();

        var today = DateOnly.FromDateTime(DateTime.Now);
        var yesterday = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        var meal1 = new Meal(userId, today, "test1");
        var meal2 = new Meal(Guid.NewGuid(), today, "test2");
        var meal3 = new Meal(userId, today,"test3");
        var meal4 = new Meal(userId, yesterday, "test4");
        var meal5 = new Meal(userId, today, "test5");

        await AddAsync(meal1);
        await AddAsync(meal2);
        await AddAsync(meal3);
        await AddAsync(meal4);
        await AddAsync(meal5);

        var query = new GetMealsQuery() 
        {
            ForDate = today
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result?.Should().HaveCount(3);
    }
}
