using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using Dietonator.Domain.Enums;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Meals.Commands.CreateMeal;

public class CreateMealCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public MealTypeEnum Type { get; set; }
    public Guid MealPlanId { get; set; }
}

public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateMealCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var mealPlan = await GetMealPlanAsync(request.MealPlanId, cancellationToken);

        var meal = CreateMeal(request);

        mealPlan.AddMeal(meal);

        await _context.SaveChangesAsync(cancellationToken);

        return meal.Id;
    }

    private async Task<MealPlan> GetMealPlanAsync(Guid mealPlanId, CancellationToken cancellationToken)
        => await _context.MealPlans.FirstOrDefaultAsync(c => c.Id == mealPlanId, cancellationToken) 
            ?? throw new NotFoundException(nameof(MealPlan), mealPlanId);

    private Meal CreateMeal(CreateMealCommand request) 
    {
        var meal = new Meal(request.Name ?? "", request.Type);

        _context.Meals.Add(meal);

        return meal;
    }
}
