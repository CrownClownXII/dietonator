using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using MediatR;

namespace Dietonator.Application.MealPlans.Commands.CreateMealPlan;

public class CreateMealPlanCommand : IRequest<Guid>
{
    public DateOnly ForDate { get; set; }
}

public class CreateMealPlanCommandHandler: IRequestHandler<CreateMealPlanCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateMealPlanCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateMealPlanCommand request, CancellationToken cancellationToken)
    {
        var mealPlan = new MealPlan(
            request.ForDate, 
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        );

        _context.MealPlans.Add(mealPlan);

        await _context.SaveChangesAsync(cancellationToken);

        return mealPlan.Id;
    }
}
