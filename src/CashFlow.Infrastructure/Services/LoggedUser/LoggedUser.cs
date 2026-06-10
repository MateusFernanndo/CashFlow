using CashFlow.Domain.Entities;
using CashFlow.Domain.Services.LoggerUser;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CashFlow.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly CashFlowDBContext _dbContext;
    private readonly ITokenProvider _tokenProvider;
    public LoggedUser(CashFlowDBContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    } 
    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();
        var tokenHeandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHeandler.ReadJwtToken(token);
        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
