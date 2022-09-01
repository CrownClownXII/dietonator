using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Mappings;
using Dietonator.Domain.Entities;

namespace Dietonator.Application.Products.Queries;

public class ProductListDto : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
}
