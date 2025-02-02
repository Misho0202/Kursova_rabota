using FluentValidation;
using Tanks.Domain.DTOs;

namespace Tanks.Common.Validation
{
    public class BattleValidator : AbstractValidator<BattleDto>
    {
        public BattleValidator()
        {
            RuleFor(b => b.Tank1Id).NotEmpty();
            RuleFor(b => b.Tank2Id).NotEmpty();
        }
    }
}
