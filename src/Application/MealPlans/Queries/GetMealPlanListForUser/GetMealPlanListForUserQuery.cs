using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dietonator.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.MealPlans.Queries.GetMealPlanListForUser;

public class GetMealPlanListForUserQuery : IRequest<IEnumerable<MealPlanListDto>>
{
}

public class GetMealPlanListForUserQueryHandler : IRequestHandler<GetMealPlanListForUserQuery, IEnumerable<MealPlanListDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetMealPlanListForUserQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<MealPlanListDto>> Handle(GetMealPlanListForUserQuery request, CancellationToken cancellationToken)
    {
        var meals = await _context.MealPlans
            .Where(c => c.ForUser == _currentUserService.UserId)
            .ProjectTo<MealPlanListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return meals;
    }
}