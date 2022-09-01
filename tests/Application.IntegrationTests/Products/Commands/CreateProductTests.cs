using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Products.Commands.CreateProduct;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.Products.Commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowValidationException()
    {
        var command = new CreateProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateProductCommand
        {
            Name = "Test",
            Kcal = 100,
            Proteins = 10.0f,
            Carbohydrates = 10.0f,
            Fats = 10.0f,
        };

        var productId = await SendAsync(command);

        var item = await FindAsync<Product>(productId);

        item.Should().NotBeNull();
        item!.Name.Should().Be(command.Name);
        item.Kcal.Should().Be(command.Kcal);
        item.Carbohydrates.Should().Be(command.Carbohydrates);
        item.Proteins.Should().Be(command.Proteins);
        item.Fats.Should().Be(command.Fats);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
