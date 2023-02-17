using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.FaqManagement.Command.UpdateFaq;
using BoilerPlate.Core.Responses;
using BoilerPlate.Domain;
using BoilerPlate.Domain.Entities.Faq;
using BoilerPlate.Shared.Abstraction.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Core.FaqManagement.Command.DeleteFaq
{
    public sealed class DeleteFaqCommandHandler : ICommandHandler<DeleteFaqCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IFaqService _faqService;
        private readonly IContext _context;
        private readonly IClock _clock;

        public DeleteFaqCommandHandler(IDbContext dbContext, IFaqService faqService, IContext context, IClock clock)
        {
            _dbContext = dbContext;
            _faqService = faqService;
            _context = context;
            _clock = clock;
        }

        public async ValueTask<Result> Handle(DeleteFaqCommand command, CancellationToken cancellationToken)
        {
            if (!_context.Identity.Roles.Any(p => p == RoleConstant.Administrator))
            {
                return Result.Failure(Error.Create("ExCU001", "Only Administrator can Add FAQ!"));
            }

            var getFaq = await _faqService.GetFaqHeadById(command.FaqHeadId, cancellationToken);
            if (getFaq is null)
                return Result.Failure<FaqResponse>(Error.Create(404, "ExGUI001", "Data FAQ not found."));

            var getContentFaq = await _faqService.GetFaqContentById(getFaq.FqActiveContentId, cancellationToken);
            if (getContentFaq is null)
                return Result.Failure<FaqResponse>(Error.Create(404, "ExGUI001", "Active FAQ could not be found."));

            getFaq.DeletedByName = _context.Identity.Username;
            getFaq.DeletedByDt = _clock.CurrentDate();
            getFaq.DeletedByDtServer = _clock.CurrentServerDate();

            getContentFaq.DeletedByName = _context.Identity.Username;
            getContentFaq.DeletedByDt = _clock.CurrentDate();
            getContentFaq.DeletedByDtServer = _clock.CurrentServerDate();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
