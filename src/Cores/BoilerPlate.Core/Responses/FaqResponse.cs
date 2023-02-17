using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Core.Responses
{
    public sealed class FaqResponse
    {
        public FaqResponse() { }

        public Guid FqContentId { get; set; }
        public string? FqTitle { get; set; }
        public string? FqContent { get; set; }
        public Guid FqHeadId { get; set; }
    }

    public sealed class FaqHeadResponse
    {
        public FaqHeadResponse() { }
        public Guid FqHeadId { get; set; }
        public string? FqTitle { get; set; }
    }
}
