using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Response;
public class ResponseExpensesJson
{
    public long Id { get; set; }
    public string Title { get; set;} = string.Empty;
    public string? Description {  get; set;}
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentsType PaymentsType { get; set; }
}