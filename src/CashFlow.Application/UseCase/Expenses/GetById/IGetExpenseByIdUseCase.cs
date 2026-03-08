using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Expenses.GetById;
public interface IGetExpenseByIdUseCase
{
    public Task<ResponseExpensesJson> Execute(long id);
}
