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

namespace Dietonator.Application.Meals.Commands.UpdateMealProduct;

public class UpdateMealProductCommand : IRequest
{
    public Guid MealId { get; set; }
    public Guid MealProductId { get; set; }
    public int Amount { get; set; }
}

public class UpdateMealProductCommandHandler : IRequestHandler<UpdateMealProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMealProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateMealProductCommand request, CancellationToken cancellationToken)
    {
        var mealProduct = await _context.MealProducts
            .FirstOrDefaultAsync(c => c.Id == request.MealProductId, cancellationToken);

        if (mealProduct == null)
        {
            throw new NotFoundException(nameof(MealProduct), request.MealProductId);
        }

        mealProduct.Amount = request.Amount;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
