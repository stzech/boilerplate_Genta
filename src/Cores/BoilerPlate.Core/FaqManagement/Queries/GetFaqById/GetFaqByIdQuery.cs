using BoilerPlate.Core.Responses;

namespace BoilerPlate.Core.FaqManagement.Queries.GetFaqById
{
    public sealed record GetFaqByIdQuery : IQuery<Result<FaqResponse>>
    {
        public GetFaqByIdQuery() { }
        public GetFaqByIdQuery(Guid _FaqHeadId)
        {
            FaqHeadId = _FaqHeadId;
        }
        public Guid FaqHeadId { get; set; }
    }
}
