using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using Dietonator.Domain.Enums;
using MediatR;

namespace Dietonator.Application.Meals.Commands.CreateMeal;

public class CreateMealCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public MealTypeEnum Type { get; set; }
}

public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateMealCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = new Meal(request.Name, request.Type);

        _context.Meals.Add(meal);

        await _context.SaveChangesAsync(cancellationToken);

        return meal.Id;
    }
}
