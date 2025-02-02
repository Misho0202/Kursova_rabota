using FluentValidation;
using Tanks.Domain.DTOs;

namespace Tanks.Common.Validation
{
    public class TankValidator : AbstractValidator<TankDto>
    {
        public TankValidator()
        {
            RuleFor(t => t.Name).NotEmpty().MaximumLength(100);
            RuleFor(t => t.Health).GreaterThan(0);
        }
    }
}
