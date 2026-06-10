using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
public class CashFlowDBContext : DbContext
{
    public CashFlowDBContext(DbContextOptions options) : base(options){}

    public DbSet<Expense> Expenses { get; set; } //irá fazer a conexao com o banco de dados
    public DbSet<User> Users { get; set; } //irá fazer a conexao com o banco de dadoso

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Tag>().ToTable("Tags");
    }

}
