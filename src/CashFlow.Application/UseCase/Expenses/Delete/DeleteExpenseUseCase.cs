
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggerUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCase.Expenses.Delete;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseReadOnly;
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unityOfWork;
    private readonly ILoggedUser _loggedUser;
    public DeleteExpenseUseCase(
        IExpensesWriteOnlyRepository repository, 
        IUnitOfWork unityOfWork,
        ILoggedUser loggedUser,
        IExpenseReadOnlyRepository expenseReadOnly)
    {
        _repository = repository;
        _unityOfWork = unityOfWork;
        _loggedUser = loggedUser;
        _expenseReadOnly = expenseReadOnly;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var expense = await _expenseReadOnly.GetById(loggedUser, id);
        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }
        await _repository.Delete(id);

        await _unityOfWork.Commit();
    }
}
