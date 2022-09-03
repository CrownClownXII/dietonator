using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dietonator.Application.Common.Exceptions;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.MealPlans.Queries.GetMealPlanDetails;

public class GetMealPlanDetailsQuery : IRequest<MealPlanDetailsDto>
{
    public Guid MealPlanId { get; set; }
}

public class GetMealPlanDetailsQueryHandler : IRequestHandler<GetMealPlanDetailsQuery, MealPlanDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMealPlanDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MealPlanDetailsDto> Handle(GetMealPlanDetailsQuery request, CancellationToken cancellationToken)
    {
        var meal = await _context.MealPlans
            .Include(c => c.Meals)
            .ProjectTo<MealPlanDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == request.MealPlanId, cancellationToken);

        return meal ?? throw new NotFoundException(nameof(MealPlan), request.MealPlanId);
    }
}