using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dietonator.Application.Meals.Commands.SetProductAmount;

public class SetProductAmountCommandValidator : AbstractValidator<SetProductAmountCommand>
{
    public SetProductAmountCommandValidator()
    {
        RuleFor(v => v.Amount)
            .GreaterThanOrEqualTo(0);
    }
}
