namespace CashFlow.Application.UseCase.Expenses.Reports.PDF;

public interface IGenerateExpensesReportPdfUseCase
{
    public Task<byte[]> Execute(DateOnly month);
}
