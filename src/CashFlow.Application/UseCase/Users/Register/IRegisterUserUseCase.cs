using CashFlow.Communication.Request;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}
