using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Products.Queries;
using Dietonator.Application.Statistic.Queries.GetUserStatistic;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.Products.Queries;

using static Testing;

public class GetUserStatisticQueryTest : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnStatisticForUser()
    {
        var userId = await RunAsDefaultUserAsync();

        var firstDay = DateOnly.FromDateTime(DateTime.Now.AddDays(-4));
        var secondDay = DateOnly.FromDateTime(DateTime.Now.AddDays(-3));
        var thirdDay = DateOnly.FromDateTime(DateTime.Now.AddDays(-2));
        var fourthDay = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        var lastDay = DateOnly.FromDateTime(DateTime.Now);

        var product1 = new Product("test", 10, 10.0f, 10.0f, 10.0f);
        var product2 = new Product("test", 12, 12.0f, 12.0f, 12.0f);
        var product3 = new Product("test", 12, 12.0f, 12.0f, 12.0f);
        var product4 = new Product("test", 12, 12.0f, 12.0f, 12.0f);
        var product5 = new Product("test", 12, 12.0f, 12.0f, 12.0f);
        var product6 = new Product("test", 12, 12.0f, 12.0f, 12.0f);
        var product7 = new Product("test", 12, 12.0f, 12.0f, 12.0f);
        var product8 = new Product("test", 12, 12.0f, 12.0f, 12.0f);

        var meal1 = new Meal(userId, firstDay, "test1");
        var meal2 = new Meal(userId, secondDay, "test2");
        var meal3 = new Meal(userId, thirdDay,"test3");
        var meal4 = new Meal(userId, fourthDay, "test4");
        var meal5 = new Meal(userId, fourthDay, "test5");
        var meal6 = new Meal(userId, lastDay, "test5");
        var meal7 = new Meal(Guid.NewGuid(), lastDay, "test5");

        meal1.AddProduct(new MealProduct(product1, 100));
        meal2.AddProduct(new MealProduct(product2, 100));
        meal3.AddProduct(new MealProduct(product3, 100));
        meal4.AddProduct(new MealProduct(product4, 100));
        meal5.AddProduct(new MealProduct(product5, 100));
        meal6.AddProduct(new MealProduct(product6, 100));
        meal7.AddProduct(new MealProduct(product7, 100));
        meal1.AddProduct(new MealProduct(product8, 100));

        await AddAsync(meal1);
        await AddAsync(meal2);
        await AddAsync(meal3);
        await AddAsync(meal4);
        await AddAsync(meal5);
        await AddAsync(meal6);
        await AddAsync(meal7);

        var query = new GetUserStatisticQuery()
        {
            DateFrom = DateTime.Now.AddDays(-4),
            DateTo = DateTime.Now
        };

        var result = await SendAsync(query);

        var calories = meal4.Kcal + meal5.Kcal;

        var dayFourStatistic = result.FirstOrDefault(c => c.ForDate == fourthDay);

        result.Should().NotBeEmpty();
        result.Should().HaveCount(5);
        result.Should().HaveCount(5);
        dayFourStatistic.Should().NotBeNull();
        dayFourStatistic!.Kcal.Should().Be(calories);
    }
}
