using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dietonator.Application.Meals.Commands.UpdateMealProduct;

public class UpdateMealProductCommandValidator : AbstractValidator<UpdateMealProductCommand>
{
    public UpdateMealProductCommandValidator()
    {
        RuleFor(v => v.Amount)
            .GreaterThanOrEqualTo(0);
    }
}
