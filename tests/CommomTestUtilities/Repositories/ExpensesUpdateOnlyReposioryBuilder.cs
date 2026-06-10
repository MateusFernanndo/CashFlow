using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommomTestUtilities.Repositories;

public class ExpensesUpdateOnlyReposioryBuilder
{
    private readonly Mock<IExpensesUpdateOnlyRepository> _repository;

    public ExpensesUpdateOnlyReposioryBuilder()
    {
        _repository = new Mock<IExpensesUpdateOnlyRepository>();
    }

    public ExpensesUpdateOnlyReposioryBuilder GetById(User user, Expense? expense)
    {
        if(expense is not null)
            _repository.Setup(repository => repository.GetById(user, expense.Id)).ReturnsAsync(expense);
        return this;
    }

    public IExpensesUpdateOnlyRepository Build() => _repository.Object;
}
