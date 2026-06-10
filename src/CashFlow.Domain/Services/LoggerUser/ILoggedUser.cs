using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Services.LoggerUser;

public interface ILoggedUser
{
    Task<User> Get();
}
