using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration )
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnityOfWork>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>(); //injeção de dependencia
        services.AddScoped<IExpenseReadOnlyRepository, ExpensesRepository>(); //injeção de dependencia
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>(); //injeção de dependencia
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var conectionString = configuration.GetConnectionString("Connection"); //sintaxe que precisda para conectar no servidor
        var version = new Version(8, 0, 35);
        var serverVersion = new MySqlServerVersion(version);
                
        services.AddDbContext<CashFlowDBContext>(config => config.UseMySql(conectionString, serverVersion)); ;
    }
}

