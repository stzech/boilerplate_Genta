using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerPlate.Domain.Entities.Faq;

namespace BoilerPlate.Core.Abstractions
{
    public interface IFaqService
    {
        Task<FaqHead?> GetFaqHeadById(Guid FqHeadId, CancellationToken cancellation);
        Task<List<FaqHead>> GetFaqHeadList(CancellationToken cancellation);
        Task<FaqContent?> GetFaqContentById(Guid FqHeadId, CancellationToken cancellation);
    }
}
