using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Statistic.Queries.GetDailyUserStatistic;

public class GetDailyUserStatisticQuery : IRequest<DailyUserStatisticDto>
{
    public DateTime Date { get; set; }
}

public class GetDailyUserStatisticQueryHandler : IRequestHandler<GetDailyUserStatisticQuery, DailyUserStatisticDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;


    public GetDailyUserStatisticQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<DailyUserStatisticDto> Handle(GetDailyUserStatisticQuery request, CancellationToken cancellationToken)
    {
        var date = DateOnly.FromDateTime(request.Date);

        var mealsForDay = await _context.Meals
            .Include(c => c.Products)
                .ThenInclude(c => c.Product)
            .Where(c => c.UserId == _currentUserService.UserId 
                && date == c.ForDate)
            .ToListAsync(cancellationToken);

        var mappedMeals = _mapper.Map<IEnumerable<MealStatisticDto>>(mealsForDay);

        return new DailyUserStatisticDto 
        {
            UserId = _currentUserService.UserId ?? throw new NotFoundException("User", "null"),
            ForDate = date,
            Meals = mappedMeals,
        };
    }
}