using CashFlow.Application.UseCase.Expenses.Delete;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Expenses.Delete;

public class DeleeExpenseUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, expense);

        var act = async () => await useCase.Execute(expense.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        var useCase = CreateUseCase(loggedUser);
        var act = async () => await useCase.Execute(id: 1000);
        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND));
    }

    private DeleteExpenseUseCase CreateUseCase(CashFlow.Domain.Entities.User user, Expense? expense = null)
    {
        var repositoryWriteOnly = ExpensesWriteOnlyRepositoryBuilder.Build();
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var unitOfWork = UnitOfWorkBuider.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new DeleteExpenseUseCase(repositoryWriteOnly, unitOfWork, loggedUser, repository);
    }
}
