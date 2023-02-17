using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Core.FaqManagement.Command.CreateFaq
{
    public sealed record CreateFaqCommand : ICommand<Result>
    {
        public string FaqTitle { get; set; } = null!;
        public string FaqContent { get; set; } = null!;
    }
}
