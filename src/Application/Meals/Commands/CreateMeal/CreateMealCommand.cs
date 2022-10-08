using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using Dietonator.Domain.Enums;
using MediatR;

namespace Dietonator.Application.Meals.Commands.CreateMeal;

public class CreateMealCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public MealTypeEnum Type { get; set; }
    public DateOnly ForDate { get; set; }
}

public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateMealCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = CreateMeal(request);

        await _context.SaveChangesAsync(cancellationToken);

        return meal.Id;
    }

    private Meal CreateMeal(CreateMealCommand request) 
    {
        var meal = new Meal(
            _currentUserService.UserId ?? throw new NotFoundException("User", "null"), 
            request.ForDate, 
            request.Name ?? "", 
            request.Type
        );

        _context.Meals.Add(meal);

        return meal;
    }
}
