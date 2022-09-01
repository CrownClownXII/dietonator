using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dietonator.Application.Meals.Commands.CreateMeal;

public class CreateMealCommandValidator : AbstractValidator<CreateMealCommand>
{
    public CreateMealCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(200);
    }
}
