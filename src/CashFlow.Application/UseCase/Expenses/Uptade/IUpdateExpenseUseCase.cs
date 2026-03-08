using CashFlow.Communication.Request;

namespace CashFlow.Application.UseCase.Expenses.Uptade;

internal interface IUpdateExpenseUseCase
{
    public Task Execute(long id, RequestExpenseJson request);
}
