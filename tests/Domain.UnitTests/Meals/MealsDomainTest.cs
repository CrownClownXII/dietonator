using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Domain.UnitTests.Meals;

public class MealsDomainTest
{
    [Test]
    public void ShouldCalculateKcal()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var meal = new Meal("A");

        var mealProductA = new MealProduct(productA, 11);
        var mealProductB = new MealProduct(productB, 3);
        var mealProductC = new MealProduct(productC, 8);

        meal.AddProduct(mealProductA);
        meal.AddProduct(mealProductB);
        meal.AddProduct(mealProductC);

        var sum = mealProductA.Kcal + mealProductB.Kcal + mealProductC.Kcal;

        meal.Kcal.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateFat()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var meal = new Meal("A");

        var mealProductA = new MealProduct(productA, 11);
        var mealProductB = new MealProduct(productB, 3);
        var mealProductC = new MealProduct(productC, 8);

        meal.AddProduct(mealProductA);
        meal.AddProduct(mealProductB);
        meal.AddProduct(mealProductC);

        var sum = mealProductA.Fats + mealProductB.Fats + mealProductC.Fats;

        meal.Fats.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateProteins()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var meal = new Meal("A");

        var mealProductA = new MealProduct(productA, 11);
        var mealProductB = new MealProduct(productB, 3);
        var mealProductC = new MealProduct(productC, 8);

        meal.AddProduct(mealProductA);
        meal.AddProduct(mealProductB);
        meal.AddProduct(mealProductC);
        
        var sum = mealProductA.Proteins + mealProductB.Proteins + mealProductC.Proteins;

        meal.Proteins.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateCarbohydrates()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var meal = new Meal("A");

        var mealProductA = new MealProduct(productA, 11);
        var mealProductB = new MealProduct(productB, 3);
        var mealProductC = new MealProduct(productC, 8);

        meal.AddProduct(mealProductA);
        meal.AddProduct(mealProductB);
        meal.AddProduct(mealProductC);

        var sum = mealProductA.Carbohydrates + mealProductB.Carbohydrates + mealProductC.Carbohydrates;

        meal.Carbohydrates.Should().Be(sum);
    }
}
