using CashFlow.Application.UseCase.Users.Delete;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.User.Delete;

public class DeleteUserAccountUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUSeCase(user);

        var act = async () => await useCase.Execute();

        await act.Should().NotThrowAsync();
    }

    private DeleteUserAccountUseCase CreateUSeCase(CashFlow.Domain.Entities.User user)
    {
        var repository = UserWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuider.Build();

        return new DeleteUserAccountUseCase(loggedUser, repository, unitOfWork);
    }

}
