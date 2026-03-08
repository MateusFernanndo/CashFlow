using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess;

internal class UnityOfWork : IUnitOfWork
{
    private readonly CashFlowDBContext _dbContext;

    public UnityOfWork(CashFlowDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit() => await _dbContext.SaveChangesAsync();//persistir as informações no banco de dados
}

