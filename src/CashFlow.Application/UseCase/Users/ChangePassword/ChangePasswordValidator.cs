using CashFlow.Application.UseCase.Users.Validator;
using CashFlow.Communication.Request;
using FluentValidation;

namespace CashFlow.Application.UseCase.Users.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}
