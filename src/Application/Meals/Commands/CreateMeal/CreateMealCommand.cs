using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Application.Common.Repository.Meals;
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
    private readonly IMealRepository _mealRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateMealCommandHandler(IMealRepository mealRepository, ICurrentUserService currentUserService)
    {
        _mealRepository = mealRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = MealFor(request);

        _mealRepository.Add(meal);
        await _mealRepository.SaveChangesAsync(cancellationToken);

        return meal.Id;
    }

    private Meal MealFor(CreateMealCommand request) 
    {
        var meal = new Meal(
            _currentUserService.UserId ?? throw new NotFoundException("User", "null"), 
            request.ForDate, 
            request.Name ?? string.Empty, 
            request.Type
        );

        return meal;
    }
}
