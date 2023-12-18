using System;
using System.Threading;
using System.Threading.Tasks;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using Dietonator.Domain.Enums;
using Dietonator.Infrastructure.Persistence;
using Dietonator.Infrastructure.Persistence.Interceptors;
using Dietonator.Infrastructure.Persistence.Respository;
using Dietonator.Infrastructure.Services;
using Dietonator.WebUI.Services;
using FluentAssertions;
using Infrastructure.IntegrationTests.Repository.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Infrastructure.IntegrationTests.Repository.BaseRepositoryTest;

public class BaseRepositoryTest: RepositoryTestingFixture
{
    private readonly BaseRepository<Meal> _sut;

    public BaseRepositoryTest()
    {
        _sut = new BaseRepository<Meal>(_context);
    }

    [Test]
    public async Task Should_Find_Meal_When_Exisist_In_Database()
    {
        var meal = CreateMeal();

        await AddAsync(meal);

        var mealFromDb = await _sut.FindByAsync(meal.Id, CancellationToken.None);

        mealFromDb.Should().BeEquivalentTo(mealFromDb);
    }

    [Test]
    public async Task Should_Find_Null_When_Meal_Not_Exisist_In_Database()
    {
        var mealFromDb = await _sut.FindByAsync(Guid.NewGuid(), CancellationToken.None);

        Assert.Null(mealFromDb);
    }

    [Test]
    public async Task Should_Insert_Meal_To_Database()
    {
        var meal = CreateMeal();

        await InsertMealToDb(meal);

        var mealFromDb = await FindAsync<Meal>(meal.Id);

        mealFromDb.Should().BeEquivalentTo(mealFromDb);
    }

    [Test]
    public async Task Should_Remove_Meal_From_Database()
    {
        var meal = CreateMeal();

        await AddAsync(meal);

        var mealBeforeRemoving = await FindAsync<Meal>(meal.Id);
        
        await RemoveMealFromDb(meal);

        var mealAfterRemoving = await FindAsync<Meal>(meal.Id);

        Assert.IsNotNull(mealBeforeRemoving);
        Assert.IsNull(mealAfterRemoving);
    }

    private static Meal CreateMeal() 
    {
        return new Meal(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Now), "Test", MealTypeEnum.CalculableMeal);
    }

    private async Task InsertMealToDb(Meal meal) 
    {
        _sut.Add(meal);
        await _sut.SaveChangesAsync(CancellationToken.None);
    }

    private async Task RemoveMealFromDb(Meal meal) 
    {
        _sut.Remove(meal);
        await _sut.SaveChangesAsync(CancellationToken.None);
    }
}