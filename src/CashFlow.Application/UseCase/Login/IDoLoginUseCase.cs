using CashFlow.Communication.Request;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Login;

public interface IDoLoginUseCase
{
    public Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
}
