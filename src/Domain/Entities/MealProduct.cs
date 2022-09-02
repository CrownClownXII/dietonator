using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Domain.Enums;

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

    public int Amount { get; set; }
    public AmountTypeEnum AmountType { get; set; }

    public int Kcal => Product.Kcal;
    public float Proteins => Product.Proteins;
    public float Fats => Product.Fats;
    public float Carbohydrates => Product.Carbohydrates;
}
