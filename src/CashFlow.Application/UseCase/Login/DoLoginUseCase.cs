using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Crytography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCase.Login;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAcessTokenGenerator _acessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter,
        IAcessTokenGenerator acessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _acessTokenGenerator = acessTokenGenerator;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);

        if(user is null)
        {
            throw new InvalidLoginException();
        }

        var passwordMAtch = _passwordEncripter.Verify(request.Password, user.Password);

        if(passwordMAtch == false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = _acessTokenGenerator.Generate(user)
        };
    }
}
