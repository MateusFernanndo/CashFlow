using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Moq;

namespace CommomTestUtilities.Token;

public class JwtTokenGeneratorBuilder
{
    public static IAcessTokenGenerator Build()
    {
        var mock = new Mock<IAcessTokenGenerator>();

        mock.Setup(acessTokenGenerator => acessTokenGenerator.Generate(It.IsAny<User>())).Returns("token") ; //quando alguem chamar, irá devolver alguma coisa

        return mock.Object;
    }
}
