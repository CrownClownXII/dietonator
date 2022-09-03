using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.MealPlans.Queries.GetMealsList;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.MealPlans.Queries;

using static Testing;

public class GetMealPlanDetailsTest
{
    [Test]
    public async Task ShouldThrowNotFoundExceptionOnMealPlanDetails()
    {
        var command = new GetMealPlanDetailsQuery();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldReturnAllProducts()
    {
        var userId = await RunAsDefaultUserAsync();

        var mealPlan = new MealPlan(DateOnly.FromDateTime(DateTime.Now), userId);
        var mealPlan2 = new MealPlan(DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), userId);
        var mealPlan3 = new MealPlan(DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), userId);

        var meal1 = new Meal("test1");
        var meal2 = new Meal("test2");
        var meal3 = new Meal("test3");
        var meal4 = new Meal("test4");
        var meal5 = new Meal("test5");

        mealPlan.AddMeal(meal1);
        mealPlan.AddMeal(meal2);
        mealPlan.AddMeal(meal3);

        mealPlan2.AddMeal(meal4);

        mealPlan3.AddMeal(meal5);

        await AddAsync(mealPlan);
        await AddAsync(mealPlan2);
        await AddAsync(mealPlan3);

        var query = new GetMealPlanDetailsQuery()
        {
            MealPlanId = mealPlan.Id
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Meals.Should().HaveCount(3);
    }
}
