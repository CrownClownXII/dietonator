using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Products.Queries;
using Dietonator.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Dietonator.Application.IntegrationTests.Products.Queries;

using static Testing;

public class GetProductListTest
{
    [Test]
    public async Task ShouldReturnAllProducts()
    {
        await AddAsync(new Product("test", 11, 11.0f, 12.0f, 13.0f));

        var query = new GetProductListQuery();

        var result = await SendAsync(query);

        result.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnFiltratedProducts()
    {
        await AddAsync(new Product("tebstbAAtest", 11, 11.0f, 12.0f, 13.0f));
        await AddAsync(new Product("testBBtest", 11, 11.0f, 12.0f, 13.0f));
        await AddAsync(new Product("testAAtestBB", 11, 11.0f, 12.0f, 13.0f));
        await AddAsync(new Product("BBtestAAtest", 11, 11.0f, 12.0f, 13.0f));

        var query = new GetProductListQuery()
        {
            SearchBy = "bb"
        };

        var result = await SendAsync(query);

        result.Should().HaveCount(3);
    }
}
