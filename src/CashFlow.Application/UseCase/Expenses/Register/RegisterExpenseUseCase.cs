using AutoMapper;
using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggerUser;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCase.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unityOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterExpenseUseCase(
        IExpensesWriteOnlyRepository repository,
        IUnitOfWork unityOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
    {
        Validade(request);
        var loggedUser = await _loggedUser.Get();
        var expense = _mapper.Map<Expense>(request);
        expense.UserId = loggedUser.Id;

        await _repository.Add(expense);
        await _unityOfWork.Commit();
        return _mapper.Map<ResponseRegisterExpenseJson>(expense);
    }
    private void Validade(RequestExpenseJson request)
    {   
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);
        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList(); //lsta de erros LINQ
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
