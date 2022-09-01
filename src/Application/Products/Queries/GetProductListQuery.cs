using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dietonator.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Products.Queries;

public class GetProductListQuery : IRequest<IEnumerable<ProductListDto>>
{
    public string? SearchBy { get; set; }
}

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<ProductListDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductListDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var searchBy = request.SearchBy?.Trim()?.ToLower() ?? string.Empty;

        var products = await _context.Products
            .Where(c => c.Name.Trim().ToLower().Contains(searchBy))
            .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return products;
    }
}

