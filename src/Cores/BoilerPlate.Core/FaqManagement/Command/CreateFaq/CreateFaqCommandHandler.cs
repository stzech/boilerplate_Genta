using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.UserManagement.Commands.CreateUser;
using BoilerPlate.Domain;
using BoilerPlate.Domain.Entities.Faq;

namespace BoilerPlate.Core.FaqManagement.Command.CreateFaq
{
    public sealed class CreateFaqCommandHandler : ICommandHandler<CreateFaqCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IClock _clock;
        private readonly IContext _context;
        public CreateFaqCommandHandler(IDbContext dbContext, IClock clock, IContext context)
        {
            _dbContext = dbContext;
            _clock = clock;
            _context = context;
        }

        /// <summary>
        /// Create a new Faq
        /// </summary>
        /// <param name="request">See <see cref="CreateFaqCommand"/></param>
        /// <param name="cancellationToken">See <see cref="CancellationToken"/></param>
        /// <returns>See <see cref="Result"/></returns>
        public async ValueTask<Result> Handle(CreateFaqCommand command, CancellationToken cancellationToken)
        {
            if(!_context.Identity.Roles.Any(p => p == RoleConstant.Administrator))
            {
                return Result.Failure(Error.Create("ExCU001", "Only Administrator can Add FAQ!"));
            }

            if(command.FaqTitle.Length > 100)
            {
                return Result.Failure(Error.Create("ExCU001", "Title cannot exceed 100 characters!"));
            }

            var newFaqContent = new FaqContent()
            {
                FqTitle = command.FaqTitle,
                FqContent = command.FaqContent,
                CreatedByName = _context.Identity.Username,
                CreatedDt = _clock.CurrentDate(),
                CreatedDtServer = _clock.CurrentServerDate(),
            };

            var newFaqHead = new FaqHead()
            {
                FqActiveContentId = newFaqContent.FqContentId,
                CreatedByName = _context.Identity.Username,
                CreatedDt = _clock.CurrentDate(),
                CreatedDtServer = _clock.CurrentServerDate(),
            };
            _dbContext.Insert(newFaqHead);

            newFaqContent.FqHeadId = newFaqHead.FqHeadId;
            _dbContext.Insert(newFaqContent);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
