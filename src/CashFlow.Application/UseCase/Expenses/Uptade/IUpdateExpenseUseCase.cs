using CashFlow.Communication.Request;

namespace CashFlow.Application.UseCase.Expenses.Uptade;

public interface IUpdateExpenseUseCase
{
    public Task Execute(long id, RequestExpenseJson request);
}
