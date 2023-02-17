using BoilerPlate.Core.Abstractions;
using Moq;

namespace BoilerPlate.UnitTests.Extensions
{
    public static class FaqServiceBuilderExtensions
    {
        public static Mock<IFaqService> Create()
        {
            var mock = new Mock<IFaqService>();
            return mock;
        }
    }
}
