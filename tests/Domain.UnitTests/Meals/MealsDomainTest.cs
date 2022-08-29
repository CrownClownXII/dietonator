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

        meal.AddProduct(productA);
        meal.AddProduct(productB);
        meal.AddProduct(productC);

        var sum = productA.Kcal + productB.Kcal + productC.Kcal;

        meal.Kcal.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateFat()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var meal = new Meal("A");

        meal.AddProduct(productA);
        meal.AddProduct(productB);
        meal.AddProduct(productC);

        var sum = productA.Fats + productB.Fats + productC.Fats;

        meal.Fats.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateProteins()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var meal = new Meal("A");

        meal.AddProduct(productA);
        meal.AddProduct(productB);
        meal.AddProduct(productC);

        var sum = productA.Proteins + productB.Proteins + productC.Proteins;

        meal.Proteins.Should().Be(sum);
    }

    [Test]
    public void ShouldCalculateCarbohydrates()
    {
        var productA = new Product("A", 11, 12.3f, 11.2f, 33.3f);
        var productB = new Product("B", 32, 12.3f, 11.2f, 33.3f);
        var productC = new Product("C", 3, 12.3f, 11.2f, 33.3f);

        var meal = new Meal("A");

        meal.AddProduct(productA);
        meal.AddProduct(productB);
        meal.AddProduct(productC);

        var sum = productA.Carbohydrates + productB.Carbohydrates + productC.Carbohydrates;

        meal.Carbohydrates.Should().Be(sum);
    }
}
