using AutoMapper;
using Dietonator.Application.Common.Mappings;
using Dietonator.Domain.Entities;

namespace Dietonator.Application.Meals.Queries.GetMealsForDate;

public class MealDto : IMapFrom<Meal>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }

    public IEnumerable<MealProductDto> Products { get; set; } = new List<MealProductDto>();

    public void Mapping(Profile profile) 
    {
        profile.CreateMap<Meal, MealDto>()
            .ForMember(c => c.Products, src => src.MapFrom(x => x.Products.Select(c => c.Product)));
    }
}

public class MealProductDto: IMapFrom<Product> 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
}