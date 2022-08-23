using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.WebUI.Controllers;

public class ProductController : ApiControllerBase
{
    private readonly IApplicationDbContext _context;

    public ProductController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        return await _context.Products.ToListAsync();
    }
}
