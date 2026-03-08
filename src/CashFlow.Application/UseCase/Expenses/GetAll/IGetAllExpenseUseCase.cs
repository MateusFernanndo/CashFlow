using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Expenses.GetAll;

public interface IGetAllExpenseUseCase
{
    Task<ResponseExpensesJson> Execute();
}
