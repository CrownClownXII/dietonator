using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Dietonator.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected bool IdentifiersNotEqual(Guid queryId, Guid modelId) 
    {
        return queryId != modelId;
    }
}
