using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Crytography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoggerUser;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Extensions;
using CashFlow.Infrastructure.Security.Tokens;
using CashFlow.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration )
    {
        services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        
        AddToken(services, configuration);
        AddRepositories(services);
        
        if (configuration.IsTestEnvironment() == false)
        {
            AddDbContext(services, configuration);
        }
    }

    private static void AddToken(IServiceCollection services,  IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAcessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnityOfWork>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>(); //injeção de dependencia
        services.AddScoped<IExpenseReadOnlyRepository, ExpensesRepository>(); //injeção de dependencia
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>(); //injeção de dependencia
        services.AddScoped<IUserReadOnlyRepository, UserRepository>(); //injeção de dependencia
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>(); //injeção de dependencia
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>(); //injeção de dependencia
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var conectionString = configuration.GetConnectionString("Connection"); //sintaxe que precisda para conectar no servidor
        
        var serverVersion = ServerVersion.AutoDetect(conectionString);
                
        services.AddDbContext<CashFlowDBContext>(config => config.UseMySql(conectionString, serverVersion));
    }
}

