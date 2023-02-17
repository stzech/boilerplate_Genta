using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Core.FaqManagement.Command.DeleteFaq
{
    public sealed record DeleteFaqCommand : ICommand<Result>
    {
        public DeleteFaqCommand() { }
        public Guid FaqHeadId { get; set; }
    }
}
