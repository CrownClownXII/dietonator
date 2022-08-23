using Dietonator.Application.Common.Interfaces;

namespace Dietonator.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
