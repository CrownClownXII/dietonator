using AutoMapper;
using Dietonator.Application.Common.Mappings;
using Dietonator.Domain.Entities;

namespace Dietonator.Application.Statistic.Queries.GetDailyUserStatistic;

public class DailyUserStatisticDto
{
    public Guid UserId { get; set; }
    public DateOnly ForDate { get; set; }
    public float Kcal => Meals.Sum(c => c.Kcal);
    public float Proteins => Meals.Sum(c => c.Proteins);
    public float Fats => Meals.Sum(c => c.Fats);
    public float Carbohydrates => Meals.Sum(c => c.Carbohydrates);
    public IEnumerable<MealStatisticDto> Meals { get; set; } = new List<MealStatisticDto>();
}

public class MealStatisticDto : IMapFrom<Meal>
{
    public Guid Id { get; set; }
    public float Kcal => Products.Sum(c => c.Kcal);
    public float Proteins => Products.Sum(c => c.Proteins);
    public float Fats => Products.Sum(c => c.Fats);
    public float Carbohydrates => Products.Sum(c => c.Carbohydrates);
    public IEnumerable<ProductStatisticDto> Products { get; set; } = new List<ProductStatisticDto>();
}

public class ProductStatisticDto : IMapFrom<MealProduct>
{
    public Guid Id { get; set; }
    public Guid MealId { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = "";
    public int Amount { get; set; }
    public float Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }

    public void Mapping(Profile profile)
    {
         profile.CreateMap<MealProduct, ProductStatisticDto>()
            .ForMember(c => c.MealId, src => src.MapFrom(x => x.MealId))
            .ForMember(c => c.ProductId, src => src.MapFrom(x => x.Product.Id))
            .ForMember(c => c.Name, src => src.MapFrom(x => x.Product.Name))
            .ForMember(c => c.Kcal, src => src.MapFrom(x => x.Product.Kcal * x.Amount))
            .ForMember(c => c.Proteins, src => src.MapFrom(x => x.Product.Proteins * (float)x.Amount))
            .ForMember(c => c.Fats, src => src.MapFrom(x => x.Product.Fats * x.Amount))
            .ForMember(c => c.Carbohydrates, src => src.MapFrom(x => x.Product.Carbohydrates * x.Amount));
    }
}

