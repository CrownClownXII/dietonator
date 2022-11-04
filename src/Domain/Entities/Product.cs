using Dietonator.Domain.Enums;

namespace Dietonator.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public Product(string name, float kcal, float proteins, float fats, float carbohydrates)
    {
        Id = Guid.NewGuid();
        Name = name;
        Kcal = kcal;    
        Proteins = proteins;
        Fats = fats;
        Carbohydrates = carbohydrates;
    }

    public string Name { get; set; }
    public AmountTypeEnum AmountType { get; set; }
    
    public float Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
}
