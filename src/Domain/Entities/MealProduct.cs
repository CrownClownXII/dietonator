namespace Dietonator.Domain.Entities;

public class MealProduct: BaseAuditableEntity
{
    public MealProduct(Product product, int amount)
    {
        Id = Guid.NewGuid();
        Product = product;
        Amount = amount;
    }

    protected MealProduct() { }

    public Product Product { get; set; }
    public Guid MealId { get; set; }

    public int Amount { get; set; }

    public float Kcal => Product.Kcal * Amount;
    public float Proteins => Product.Proteins * Amount;
    public float Fats => Product.Fats * Amount;
    public float Carbohydrates => Product.Carbohydrates * Amount;
}
