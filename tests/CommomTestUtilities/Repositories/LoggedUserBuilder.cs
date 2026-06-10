using CashFlow.Domain.Entities;
using CashFlow.Domain.Services.LoggerUser;
using Moq;

namespace CommomTestUtilities.Repositories;

public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();
        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user); 

        return mock.Object;

    }
}
