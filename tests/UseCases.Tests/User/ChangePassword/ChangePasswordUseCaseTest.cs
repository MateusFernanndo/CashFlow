using CashFlow.Application.UseCase.Users.ChangePassword;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Tests.User.ChangePassword;

public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);
        var act = async () => await useCase.Execute(request);
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        var user = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = string.Empty;

        var useCase = CreateUseCase(user, request.Password);
        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(e => e.GetErrors().Count == 1 &&
                e.GetErrors().Contains(ResourceErrorMessages.INVALID_PASSWORD));
    }

    [Fact]
    public async Task Error_CurrentPassword_Different()
    {
        var user = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(e => e.GetErrors().Count == 1 &&
                e.GetErrors().Contains(ResourceErrorMessages.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
    }




    private static ChangePasswordUseCase CreateUseCase(CashFlow.Domain.Entities.User user, string? password = null)
    {
        var unitOfWork = UnitOfWorkBuider.Build();
        var userUpdateRepository = UserUpdateOnlyRepositoryBuilder.Build(user);
        var loggedUSer = LoggedUserBuilder.Build(user);
        var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();

        return new ChangePasswordUseCase(loggedUSer, userUpdateRepository, unitOfWork, passwordEncrypter);

    }
}
