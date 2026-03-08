using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;

namespace CashFlow.Domain.Extensions;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentsType paymentType )
    {
        return paymentType switch
        {
            PaymentsType.Cash => ResourceReportGenerationMessages.CASH,
            PaymentsType.CreditCArd => ResourceReportGenerationMessages.CREDIT_CARD,
            PaymentsType.DebitCard => ResourceReportGenerationMessages.DEBIT_CARD,
            PaymentsType.EletronicTransfer => ResourceReportGenerationMessages.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}
