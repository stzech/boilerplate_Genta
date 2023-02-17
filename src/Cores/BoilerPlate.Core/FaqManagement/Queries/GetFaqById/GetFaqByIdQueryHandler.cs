using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Responses;
using BoilerPlate.Domain.Entities;

namespace BoilerPlate.Core.FaqManagement.Queries.GetFaqById
{
    public sealed class GetFaqByIdQueryHandler : IQueryHandler<GetFaqByIdQuery, Result<FaqResponse>>
    {
        private readonly IFaqService _faqService;
        public GetFaqByIdQueryHandler(IFaqService faqService)
        {
            _faqService = faqService;
        }

        public async ValueTask<Result<FaqResponse>> Handle(GetFaqByIdQuery query, CancellationToken cancellationToken)
        {
            var getFaq = await _faqService.GetFaqHeadById(query.FaqHeadId, cancellationToken);
            if (getFaq is null)
                return Result.Failure<FaqResponse>(Error.Create(404, "ExGUI001", "Data FAQ not found."));

            var getContentFaq = await _faqService.GetFaqContentById(getFaq.FqActiveContentId, cancellationToken);
            if (getContentFaq is null)
                return Result.Failure<FaqResponse>(Error.Create(404, "ExGUI001", "Active FAQ could not be found."));

            var FaqResult = new FaqResponse();
            FaqResult.FqContentId = getContentFaq.FqContentId;
            FaqResult.FqTitle = getContentFaq.FqTitle;
            FaqResult.FqContent = getContentFaq.FqContent;
            FaqResult.FqHeadId = getContentFaq.FqHeadId;

            return FaqResult;
        }
    }
}
