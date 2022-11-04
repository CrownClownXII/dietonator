using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Statistic.Queries.GetUserStatistic;

public class GetUserStatisticQuery : IRequest<IEnumerable<UserStatisticDto>>
{
    public DateOnly DateFrom { get; set; }

    public DateOnly DateTo { get; set; }
}

public class GetUserStatisticQueryHandler : IRequestHandler<GetUserStatisticQuery, IEnumerable<UserStatisticDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;


    public GetUserStatisticQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<UserStatisticDto>> Handle(GetUserStatisticQuery request, CancellationToken cancellationToken)
    {
        //Group by DateOnly cannot be correctly translate to sql in net 6 :/ - so group by is called on client collection
        var groupOfMeals = (await _context.Meals
            .Include(c => c.Products)
                .ThenInclude(c => c.Product)
            .Where(c => c.UserId == _currentUserService.UserId 
                && request.DateFrom <= c.ForDate
                && request.DateTo >= c.ForDate)
            .OrderByDescending(c => c.ForDate)
            .ToListAsync(cancellationToken))
            .GroupBy(c => c.ForDate);   

        var result = GetStatisticsForDateRange(request, groupOfMeals);

        return result;
    }

    private IEnumerable<UserStatisticDto> GetStatisticsForDateRange(GetUserStatisticQuery request, IEnumerable<IGrouping<DateOnly, Meal>> groupOfMeals)
    {
        var currentDate = request.DateFrom;

        while (currentDate <= request.DateTo)
        {
            var mealDate = currentDate;
            var mealsForDate = groupOfMeals.FirstOrDefault(c => c.Key == mealDate);

            currentDate = currentDate!.AddDays(1);

            yield return mealsForDate != null 
                ? MapMealToUserStatistic(mealsForDate)
                : GetEmptyUserStatistic(mealDate);
        }
    }

    private UserStatisticDto MapMealToUserStatistic(IGrouping<DateOnly, Meal> mealsForDate) 
    {
        var calories = mealsForDate.Sum(c => c.Kcal);
        var proteins = mealsForDate.Sum(c => c.Proteins);
        var carbohydrates = mealsForDate.Sum(c => c.Carbohydrates);
        var fats = mealsForDate.Sum(c => c.Fats);

        return new UserStatisticDto 
        {
            UserId = _currentUserService.UserId ?? throw new NotFoundException("User", "null"),
            ForDate = mealsForDate.Key,
            Kcal = calories,
            Carbohydrates = carbohydrates,
            Proteins = proteins,
            Fats = fats
        };
    }

    private UserStatisticDto GetEmptyUserStatistic(DateOnly forDate) 
    {
        return new UserStatisticDto 
        {
            UserId = _currentUserService.UserId ?? throw new NotFoundException("User", "null"),
            ForDate = forDate,
        };
    }
}

