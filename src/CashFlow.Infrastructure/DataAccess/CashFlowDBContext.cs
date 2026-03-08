using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
internal class CashFlowDBContext : DbContext
{
    public CashFlowDBContext(DbContextOptions options) : base(options){}

    public DbSet<Expense> Expenses { get; set; } //irá fazer a conexao com o banco de dados

}
