using BoilerPlate.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Core.FaqManagement.Queries.GetFaqList
{
    public sealed record GetFaqListQuery : IQuery<Result<List<FaqHeadResponse>>>
    {
        public GetFaqListQuery() { }
    }
}
