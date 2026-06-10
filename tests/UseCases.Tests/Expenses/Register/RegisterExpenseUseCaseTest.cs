using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Tests.Expenses.Register;

public class RegisterExpenseUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);
        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.TITLE_REQUIRED));

    }

    private RegisterExpenseUseCase CreateUseCase(CashFlow.Domain.Entities.User user)
    {
        var repository = ExpensesWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuider.Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);


        return new RegisterExpenseUseCase(repository, unitOfWork, mapper, loggedUser);
    }
}
