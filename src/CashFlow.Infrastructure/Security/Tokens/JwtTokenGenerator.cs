using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Infrastructure.Security.Tokens;

public class JwtTokenGenerator : IAcessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()), //Sid = Security Identifier
            new Claim(ClaimTypes.Role, user.Role) //quem tem autorização
        };

        var tokenDescriptor = new SecurityTokenDescriptor //descrevendo o token
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes), //data que expira
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature), //chave que iá usar para assinar o token
            Subject = new ClaimsIdentity(claims) //colocar as propriedades desejadas no token
        };

        var tokenHandler = new JwtSecurityTokenHandler(); //criar um token
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken); //devolver o token com string
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);
        return new SymmetricSecurityKey(key);
    }
}
