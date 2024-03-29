using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Identity.Commands.SignIn;
using BoilerPlate.IntegrationTests.Fixtures;
using BoilerPlate.Persistence;
using BoilerPlate.Shared.Abstraction.Auth;
using BoilerPlate.Shared.Abstraction.Storage;
using BoilerPlate.Shared.Abstraction.Time;
using BoilerPlate.Shared.Infrastructure.Auth;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace BoilerPlate.IntegrationTests.Handlers;

[Collection(Constant.ServiceCollectionDefaultName)]
public class SignInHandlerTests : IClassFixture<ServiceFixture>
{
    public const string Username = "admin";
    public const string Password = "Qwerty@1234";

    private readonly SignInCommandHandler _ctor;

    public SignInHandlerTests(ServiceFixture serviceFixture)
    {
        _ctor = new SignInCommandHandler(
            serviceFixture.ServiceProvider.GetRequiredService<IUserService>(),
            serviceFixture.ServiceProvider.GetRequiredService<IClock>(),
            serviceFixture.ServiceProvider.GetRequiredService<SqlServerDbContext>(),
            new AuthOptions
            {
                Expiry = new TimeSpan(7, 0, 0)
            },
            serviceFixture.ServiceProvider.GetRequiredService<IRequestStorage>(),
            serviceFixture.ServiceProvider.GetRequiredService<IAuthManager>());
    }

    [Fact]
    public async Task SignIn_WithInvalidPassword_ShouldReturn_Error()
    {
        const string password = "Qwerty@12345"; //default password is Qwerty@1234

        var result = await _ctor.Handle(new SignInCommand
        {
            ClientId = Guid.NewGuid().ToString(),
            Username = Username,
            Password = password
        }, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
        result.IsSuccess.ShouldBeFalse();
        result.Error.Code.ShouldBe("ExSI002");
    }

    [Fact]
    public async Task SignIn_WithCorrectParameter_ShouldReturn_Success()
    {
        var result = await _ctor.Handle(new SignInCommand
        {
            ClientId = Guid.NewGuid().ToString(),
            Username = Username,
            Password = Password
        }, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
    }
}