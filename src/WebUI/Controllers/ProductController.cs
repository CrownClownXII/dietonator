using Dietonator.Application.Common.Interfaces;
using Dietonator.Application.Products.Commands.CreateProduct;
using Dietonator.Application.Products.Queries;
using Dietonator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.WebUI.Controllers;

public class ProductController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ProductListDto>> Get([FromQuery] GetProductListQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<Guid> Post([FromBody]CreateProductCommand command)
    {
        return await Mediator.Send(command);
    }
}
