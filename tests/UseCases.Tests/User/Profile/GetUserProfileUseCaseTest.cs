using CashFlow.Application.UseCase.Users.Profile;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.User.Profile;

public class GetUserProfileUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Email.Should().Be(user.Email);

    }
    private GetUserProfileUseCase CreateUseCase(CashFlow.Domain.Entities.User user)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetUserProfileUseCase(loggedUser, mapper);
    }
}
