using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Meals.Commands.AddProductToMeal;

public class AddProductToMealCommand : IRequest<Guid>
{
    public Guid MealId { get; set; }
    public Guid ProductId { get; set; }
}

public class AddProductToMealCommandHandler : IRequestHandler<AddProductToMealCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public AddProductToMealCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddProductToMealCommand request, CancellationToken cancellationToken)
    {
        var mealProduct = await _context.MealProducts.FirstOrDefaultAsync(c 
            => c.Product.Id == request.ProductId 
                && c.Meal.Id == request.MealId, 
            cancellationToken);

        if (mealProduct != null)
        {
            return mealProduct.Id;
        }

        var meal = await GetMeal(request.MealId, cancellationToken);
        var product = await GetProduct(request.ProductId, cancellationToken);

        mealProduct = new MealProduct(product, 0);

        meal.AddProduct(mealProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return mealProduct.Id;
    }

    private async Task<Meal> GetMeal(Guid mealId, CancellationToken cancellationToken = default) =>
        await _context.Meals.FirstOrDefaultAsync(c => c.Id == mealId, cancellationToken)
            ?? throw new NotFoundException($"Meal with id {mealId} not found");

    private async Task<Product> GetProduct(Guid productId, CancellationToken cancellationToken = default) =>
        await _context.Products.FirstOrDefaultAsync(c => c.Id == productId, cancellationToken)
            ?? throw new NotFoundException($"Product with id {productId} not found");
}