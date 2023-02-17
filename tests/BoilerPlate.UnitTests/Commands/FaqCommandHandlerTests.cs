using System;
using BoilerPlate.Core.FaqManagement.Command.CreateFaq;
using BoilerPlate.Core.FaqManagement.Command.DeleteFaq;
using BoilerPlate.Core.FaqManagement.Command.UpdateFaq;
using BoilerPlate.Core.Identity.Commands.SignIn;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Domain.Entities.Faq;
using BoilerPlate.Shared.Abstraction;
using BoilerPlate.UnitTests.Extensions;
using MockQueryable.Moq;
using Moq;

namespace BoilerPlate.UnitTests.Commands
{
    public class FaqCommandHandlerTests
    {
        [Fact]
        public async Task Handler_AddFaq_ShouldReturnFailed_WhenTitle_Exceed100Char()
        {
            //Inject
            var context = ContextBuilderExtensions.Create();
            context.Object.Identity.Roles.Add("Administrator");

            var dbContext = DbContextBuilderExtensions.Create();

            var clockService = ClockBuilderExtensions.Create();

            var createFaqCommand = new CreateFaqCommand()
            {
                FaqTitle = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",
                FaqContent = "Foo Foo Foo Foo Foo Foo Foo",
            };

            var ctor = new CreateFaqCommandHandler(
                dbContext.Object,
                clockService.Object,
                context.Object);
            var result = await ctor.Handle(createFaqCommand, CancellationToken.None);

            //Should
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public async Task Handler_AddFaq_ShouldReturnFailed_WhenUser_IsNotAdministrator()
        {
            //Inject
            var context = ContextBuilderExtensions.Create();
            context.Object.Identity.Roles.Add("User"); 
            
            var dbContext = DbContextBuilderExtensions.Create();

            var clockService = ClockBuilderExtensions.Create();

            var createFaqCommand = new CreateFaqCommand()
            {
                FaqTitle = "Foo",
                FaqContent = "Foo Foo Foo",
            };

            var ctor = new CreateFaqCommandHandler(
                dbContext.Object,
                clockService.Object,
                context.Object);
            var result = await ctor.Handle(createFaqCommand, CancellationToken.None);

            //Should
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public async Task Handler_UpdateFaq_ShouldReturnFailed_WhenUser_IsNotAdministrator()
        {
            Guid headGuid = Guid.NewGuid();
            Guid contentGuid = Guid.NewGuid();
            var faqHead = new FaqHead()
            {
                FqHeadId = headGuid,
                FqActiveContentId = contentGuid,
                CreatedByName = "Administrator",
                CreatedDt = DateTime.UtcNow,
                CreatedDtServer = DateTime.UtcNow,
            };

            var faqContent = new FaqContent()
            {
                FqContentId = contentGuid,
                FqTitle = "Foo",
                FqContent = "Foo Foo Foo",
                FqHeadId = headGuid,
                CreatedByName = "Administrator",
                CreatedDt = DateTime.UtcNow,
                CreatedDtServer = DateTime.UtcNow,
            };

            //Inject
            var faqService = FaqServiceBuilderExtensions.Create();
            faqService.Setup(p => p.GetFaqHeadById(headGuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(faqHead);
            faqService.Setup(p => p.GetFaqContentById(contentGuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(faqContent);

            var context = ContextBuilderExtensions.Create();
            context.Object.Identity.Roles.Add("User");

            var dbContext = DbContextBuilderExtensions.Create();

            var clockService = ClockBuilderExtensions.Create();

            var updateFaqCommand = new UpdateFaqCommand()
            {
                FaqHeadId = headGuid,
                FaqTitle = "Foo",
                FaqContent = "Foo Foo Foo",
            };

            var ctor = new UpdateFaqCommandHandler(
                dbContext.Object,
                faqService.Object,
                context.Object,
                clockService.Object);
            var result = await ctor.Handle(updateFaqCommand, CancellationToken.None);

            //Should
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public async Task Handler_DeleteFaq_ShouldReturnFailed_WhenUser_IsNotAdministrator()
        {
            Guid headGuid = Guid.NewGuid();
            Guid contentGuid = Guid.NewGuid();
            var faqHead = new FaqHead()
            {
                FqHeadId = headGuid,
                FqActiveContentId = contentGuid,
                CreatedByName = "Administrator",
                CreatedDt = DateTime.UtcNow,
                CreatedDtServer = DateTime.UtcNow,
            };

            var faqContent = new FaqContent()
            {
                FqContentId = contentGuid,
                FqTitle = "Foo",
                FqContent = "Foo Foo Foo",
                FqHeadId = headGuid,
                CreatedByName = "Administrator",
                CreatedDt = DateTime.UtcNow,
                CreatedDtServer = DateTime.UtcNow,
            };

            //Inject
            var faqService = FaqServiceBuilderExtensions.Create();
            faqService.Setup(p => p.GetFaqHeadById(headGuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(faqHead);
            faqService.Setup(p => p.GetFaqContentById(contentGuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(faqContent);

            var context = ContextBuilderExtensions.Create();
            context.Object.Identity.Roles.Add("User");

            var dbContext = DbContextBuilderExtensions.Create();

            var clockService = ClockBuilderExtensions.Create();

            var deleteFaqCommand = new DeleteFaqCommand()
            {
                FaqHeadId = headGuid
            };

            var ctor = new DeleteFaqCommandHandler(
                dbContext.Object,
                faqService.Object,
                context.Object,
                clockService.Object);
            var result = await ctor.Handle(deleteFaqCommand, CancellationToken.None);

            //Should
            result.IsFailure.ShouldBeTrue();
        }
    }
}
