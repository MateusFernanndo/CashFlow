using CashFlow.Application.UseCase.Expenses;
using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommomTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestExpenseRegisterJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();

    }

    //teste erros
    [Theory]
    [InlineData("")]
    [InlineData("            ")]
    [InlineData(null)] 
    public void Error_Title_Empty(string title)
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestExpenseRegisterJsonBuilder.Build();
        request.Title = title; //forçar o erro

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED)); //garante que a lista de mensagem tenhga apenas um erro
        
    }

    [Fact]
    public void Error_Date_Future()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestExpenseRegisterJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1); //forçar o erro

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE)); //garante que a lista de mensagem tenhga apenas um erro

    }

    [Fact]
    public void Error_Payment_Type()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestExpenseRegisterJsonBuilder.Build();
        request.PaymentType = (PaymentsType)700; //forçar o erro

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID)); //garante que a lista de mensagem tenhga apenas um erro

    }

    [Theory]
    [InlineData(0)]//valor passado no amount
    [InlineData(-1)]//valor passado no amount
    [InlineData(-2)]//valor passado no amount
    [InlineData(-7)]//valor passado no amount
    public void Error_Amount_Type(decimal amount)
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestExpenseRegisterJsonBuilder.Build();
        request.Amount = amount; //forçar o erro

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO)); //garante que a lista de mensagem tenhga apenas um erro

    }

}
