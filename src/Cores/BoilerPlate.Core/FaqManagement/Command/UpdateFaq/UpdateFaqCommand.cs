using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Core.FaqManagement.Command.UpdateFaq
{
    public sealed record UpdateFaqCommand : ICommand<Result>
    {
        public UpdateFaqCommand() { }
        public Guid FaqHeadId { get; set; }
        public string FaqTitle { get; set; } = null!;
        public string FaqContent { get; set; } = null!;
    }
}
