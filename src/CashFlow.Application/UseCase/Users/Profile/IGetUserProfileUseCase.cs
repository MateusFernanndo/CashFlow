using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Users.Profile;

public interface IGetUserProfileUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}
