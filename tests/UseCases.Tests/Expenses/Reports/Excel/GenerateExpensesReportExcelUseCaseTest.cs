using CashFlow.Application.UseCase.Expenses.Reports.Excel;
using CashFlow.Domain.Entities;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Sucess_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var useCase = CreateUseCase(loggedUser, new List<Expense>());
        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));
        result.Should().BeEmpty();
    }
    private GenerateExpensesReportExcelUseCase CreateUseCase(CashFlow.Domain.Entities.User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateExpensesReportExcelUseCase(repository, loggedUser);
    }
}
