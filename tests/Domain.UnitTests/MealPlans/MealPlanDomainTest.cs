using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Domain.UnitTests.MealPlans;

public class MealPlanDomainTest
{
    [Test]
    public void ShouldCalculateKcal()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var mealProductA = new MealProduct(productA, 11);
        var mealProductB = new MealProduct(productB, 3);
        var mealProductC = new MealProduct(productC, 8);

        var mealA = new Meal("A");

        mealA.AddProduct(mealProductA);
        mealA.AddProduct(mealProductB);

        var mealB = new Meal("B");

        mealB.AddProduct(mealProductB);
        mealB.AddProduct(mealProductC);

        var mealPlan = new MealPlan(DateOnly.FromDateTime(DateTime.Now), Guid.NewGuid());

        mealPlan.AddMeal(mealA);
        mealPlan.AddMeal(mealB);

        var sum = mealA.Kcal + mealB.Kcal;

        mealPlan.Kcal.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateFat()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var mealProductA = new MealProduct(productA, 11);
        var mealProductB = new MealProduct(productB, 3);
        var mealProductC = new MealProduct(productC, 8);

        var mealA = new Meal("A");

        mealA.AddProduct(mealProductA);
        mealA.AddProduct(mealProductB);

        var mealB = new Meal("B");

        mealB.AddProduct(mealProductB);
        mealB.AddProduct(mealProductC);

        var mealPlan = new MealPlan(DateOnly.FromDateTime(DateTime.Now), Guid.NewGuid());

        mealPlan.AddMeal(mealA);
        mealPlan.AddMeal(mealB);

        var sum = mealA.Fats + mealB.Fats;

        mealPlan.Fats.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateProteins()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var mealProductA = new MealProduct(productA, 11);
        var mealProductB = new MealProduct(productB, 3);
        var mealProductC = new MealProduct(productC, 8);

        var mealA = new Meal("A");

        mealA.AddProduct(mealProductA);
        mealA.AddProduct(mealProductB);

        var mealB = new Meal("B");

        mealB.AddProduct(mealProductB);
        mealB.AddProduct(mealProductC);

        var mealPlan = new MealPlan(DateOnly.FromDateTime(DateTime.Now), Guid.NewGuid());

        mealPlan.AddMeal(mealA);
        mealPlan.AddMeal(mealB);

        var sum = mealA.Proteins + mealB.Proteins;

        mealPlan.Proteins.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateCarbohydrates()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var mealProductA = new MealProduct(productA, 11);
        var mealProductB = new MealProduct(productB, 3);
        var mealProductC = new MealProduct(productC, 8);

        var mealA = new Meal("A");

        mealA.AddProduct(mealProductA);
        mealA.AddProduct(mealProductB);

        var mealB = new Meal("B");

        mealB.AddProduct(mealProductB);
        mealB.AddProduct(mealProductC);

        var mealPlan = new MealPlan(DateOnly.FromDateTime(DateTime.Now), Guid.NewGuid());

        mealPlan.AddMeal(mealA);
        mealPlan.AddMeal(mealB);

        var sum = mealA.Carbohydrates + mealB.Carbohydrates;

        mealPlan.Carbohydrates.Should().Be(sum);
    }
}

