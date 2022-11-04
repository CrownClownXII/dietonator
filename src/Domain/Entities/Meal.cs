using Dietonator.Domain.Enums;

namespace Dietonator.Domain.Entities;

public class Meal: BaseAuditableEntity
{
    public Meal(Guid userId, DateOnly forDate, string name, MealTypeEnum type = MealTypeEnum.CalculableMeal)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        ForDate = forDate;
        Name = name;
        Type = type;
    }

    public ICollection<MealProduct> Products { get; set; } = new List<MealProduct>();

    public string Name { get; set; }
    public MealTypeEnum Type { get; set; }
    public DateOnly ForDate { get; set; }

    public Guid UserId { get; set; }

    public void AddProduct(MealProduct product)
    {
        Products.Add(product);
    }

    public float Kcal => Products.Sum(c => c.Kcal);
    public float Proteins => Products.Sum(c => c.Proteins);
    public float Fats => Products.Sum(c => c.Fats);
    public float Carbohydrates => Products.Sum(c => c.Carbohydrates);
}
