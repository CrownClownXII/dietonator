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

namespace Dietonator.Application.Meals.Commands.SetProductAmount;

public class SetProductAmountCommand : IRequest
{
    public Guid MealProductId { get; set; }
    public int Amount { get; set; }
}

public class SetProductAmountCommandHandler : IRequestHandler<SetProductAmountCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductAmountCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SetProductAmountCommand request, CancellationToken cancellationToken)
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
