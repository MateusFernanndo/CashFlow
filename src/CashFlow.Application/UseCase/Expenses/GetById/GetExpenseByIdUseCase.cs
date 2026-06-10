using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggerUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCase.Expenses.GetById;

public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpenseReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetExpenseByIdUseCase(IExpenseReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseExpensesJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _repository.GetById(loggedUser, id);

        if (result is null) //tratar erro caso id inválido
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        return _mapper.Map<ResponseExpensesJson>(result);
    }
}
