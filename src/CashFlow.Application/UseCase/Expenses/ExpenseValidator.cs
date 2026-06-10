using CashFlow.Communication.Request;
using CashFlow.Exception;
using FluentValidation;
namespace CashFlow.Application.UseCase.Expenses;

public class ExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    public ExpenseValidator()
    {
        //Titulo obrigatorio
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        //Valor não pode ser menor ou igual a zero
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        //a data não pode ser do futuro, apenas de hoje para tras
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE);
        //Payment Type apenas os definidos
        RuleFor(expense => expense.PaymentsType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);

    }
}
