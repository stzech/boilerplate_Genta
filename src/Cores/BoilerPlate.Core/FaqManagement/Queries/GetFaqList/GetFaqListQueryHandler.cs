using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Core.FaqManagement.Queries.GetFaqList
{
    public sealed class GetFaqListQueryHandler : IQueryHandler<GetFaqListQuery, Result<List<FaqHeadResponse>>>
    {
        private readonly IFaqService _faqService;
        public GetFaqListQueryHandler(IFaqService faqService)
        {
            _faqService = faqService;
        }

        public async ValueTask<Result<List<FaqHeadResponse>>> Handle(GetFaqListQuery query, CancellationToken cancellationToken)
        {
            var getFaqHeadList = await _faqService.GetFaqHeadList(cancellationToken);
            var getFaqList = new List<FaqHeadResponse>();
            for(int i = 0; i < getFaqHeadList.Count(); i++)
            {
                var faqHead = getFaqHeadList[i];
                var faqTitle = faqHead.FaqContent.Where(p => p.FqHeadId == faqHead.FqHeadId).Select(p => p.FqTitle).FirstOrDefault();

                getFaqList.Add(new FaqHeadResponse()
                {
                    FqHeadId = faqHead.FqHeadId,
                    FqTitle = faqTitle,
                });
            }

            return getFaqList;
        }
    }
}
