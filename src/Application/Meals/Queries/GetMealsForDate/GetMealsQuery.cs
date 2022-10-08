using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Meals.Queries.GetMealsForDate;

public class GetMealsQuery : IRequest<IEnumerable<MealDto>>
{
    public DateOnly? ForDate { get; set; } 
}

public class GetMealsQueryHandler : IRequestHandler<GetMealsQuery, IEnumerable<MealDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetMealsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<MealDto>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var meals = GetMealsForUser();

        var filtratedMeals = GetFiltratedMeals(meals, request);    

        return await filtratedMeals
            .ProjectTo<MealDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    private IQueryable<Meal> GetMealsForUser() =>
         _context.Meals
            .Include(c => c.Products)
                .ThenInclude(c => c.Product)
            .Where(c => c.UserId == _currentUserService.UserId);

    private IQueryable<Meal> GetFiltratedMeals(IQueryable<Meal> meals, GetMealsQuery request) 
    {
        if (request.ForDate.HasValue) 
        {
            meals = meals.Where(c => c.ForDate == request.ForDate);
        }      

        return meals;
    }
}