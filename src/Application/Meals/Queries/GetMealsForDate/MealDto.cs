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

public class MealProductDto: IMapFrom<MealProduct> 
{
    public Guid Id { get; set; }
    public Guid MealId { get; set; }
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public string Name { get; set; } = "";
    public float Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }

    public void Mapping(Profile profile) 
    {
        profile.CreateMap<MealProduct, MealProductDto>()
            .ForMember(c => c.ProductId, src => src.MapFrom(x => x.Product.Id))
            .ForMember(c => c.Name, src => src.MapFrom(x => x.Product.Id))
            .ForMember(c => c.Kcal, src => src.MapFrom(x => x.Product.Kcal))
            .ForMember(c => c.Proteins, src => src.MapFrom(x => x.Product.Proteins))
            .ForMember(c => c.Fats, src => src.MapFrom(x => x.Product.Fats))
            .ForMember(c => c.Carbohydrates, src => src.MapFrom(x => x.Product.Carbohydrates));        
    }
}