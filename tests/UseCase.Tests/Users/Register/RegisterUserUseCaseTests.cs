using CashFlow.Application.UseCase.Users.Register;
using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;

namespace UseCase.Tests.Users.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {

        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuider.Build();
        var writeRespository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncrypter = PasswordEncrypterBuilder.Build();
        var acessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().Build();

        return new RegisterUserUseCase(mapper, passwordEncrypter, readRepository, writeRespository, unitOfWork, acessTokenGenerator);
    }
}
