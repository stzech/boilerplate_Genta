using System.Threading.Tasks;
using BoilerPlate.Core.UserManagement.Commands.CreateUser;
using BoilerPlate.Core.Validators;

namespace BoilerPlate.Core.FaqManagement.Command.CreateFaq
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateFaqCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(e => e.FaqTitle).MaximumLength(100);
        }
    }
}
