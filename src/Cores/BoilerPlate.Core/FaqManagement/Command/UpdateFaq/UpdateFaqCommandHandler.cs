using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Responses;
using BoilerPlate.Domain;
using BoilerPlate.Domain.Entities.Faq;
using BoilerPlate.Shared.Abstraction.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Core.FaqManagement.Command.UpdateFaq
{
    public sealed class UpdateFaqCommandHandler : ICommandHandler<UpdateFaqCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IFaqService _faqService;
        private readonly IContext _context;
        private readonly IClock _clock;

        public UpdateFaqCommandHandler(IDbContext dbContext, IFaqService faqService, IContext context, IClock clock)
        {
            _dbContext = dbContext;
            _faqService = faqService;
            _context = context;
            _clock = clock;
        }

        public async ValueTask<Result> Handle(UpdateFaqCommand command, CancellationToken cancellationToken)
        {
            if (!_context.Identity.Roles.Any(p => p == RoleConstant.Administrator))
            {
                return Result.Failure(Error.Create("ExCU001", "Only Administrator can Add FAQ!"));
            }

            if (command.FaqTitle.Length > 100)
            {
                return Result.Failure(Error.Create("ExCU001", "Title cannot exceed 100 characters!"));
            }

            var getFaq = await _faqService.GetFaqHeadById(command.FaqHeadId, cancellationToken);
            if (getFaq is null)
                return Result.Failure<FaqResponse>(Error.Create(404, "ExGUI001", "Data FAQ not found."));

            var getContentFaq = await _faqService.GetFaqContentById(getFaq.FqActiveContentId, cancellationToken);
            if (getContentFaq is null)
                return Result.Failure<FaqResponse>(Error.Create(404, "ExGUI001", "Active FAQ could not be found."));

            var newFaqContent = new FaqContent()
            {
                FqTitle = command.FaqTitle,
                FqContent = command.FaqContent,
                FqHeadId = getFaq.FqHeadId,
                CreatedByName = getContentFaq.CreatedByName,
                CreatedDt = getContentFaq.CreatedDt,
                CreatedDtServer = getContentFaq.CreatedDtServer,
                LastUpdatedByName = _context.Identity.Username,
                LastUpdatedDt = _clock.CurrentDate(),
                LastUpdatedDtServer = _clock.CurrentServerDate(),
            };
            _dbContext.Insert(newFaqContent);

            getFaq.FqActiveContentId = newFaqContent.FqContentId;
            getFaq.LastUpdatedByName = _context.Identity.Username;
            getFaq.LastUpdatedDt = _clock.CurrentDate();
            getFaq.LastUpdatedDtServer = _clock.CurrentServerDate();

            getContentFaq.DeletedByName = _context.Identity.Username;
            getContentFaq.DeletedByDt = _clock.CurrentDate();
            getContentFaq.DeletedByDtServer = _clock.CurrentServerDate();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
