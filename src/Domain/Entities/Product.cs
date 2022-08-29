using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietonator.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public Product(string name, int kcal, float proteins, float fats, float carbohydrates)
    {
        Id = Guid.NewGuid();
        Name = name;
        Kcal = kcal;    
        Proteins = proteins;
        Fats = fats;
        Carbohydrates = carbohydrates;
    }

    public ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public string Name { get; set; }
    public int Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
}
