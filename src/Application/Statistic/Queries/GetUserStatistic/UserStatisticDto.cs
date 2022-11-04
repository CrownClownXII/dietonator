using AutoMapper;
using Dietonator.Application.Common.Mappings;
using Dietonator.Domain.Entities;

namespace Dietonator.Application.Statistic.Queries.GetUserStatistic;

public class UserStatisticDto
{
    public Guid UserId { get; set; }
    public DateOnly ForDate { get; set; }
    public float Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
}
