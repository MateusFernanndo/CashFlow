using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Request;

namespace CommomTestUtilities.Requests;
public class RequestExpenseJsonBuilder
{
    public static RequestExpenseJson Build()
    {

        return new Faker<RequestExpenseJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentsType, faker => faker.PickRandom<PaymentsType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
        
        /*return new RequestExpenseRegisterJson
        {
            Amount = 100,
            Date = DateTime.Now.AddDays(-1),
            Description = "description",
            Title = "Apple",
            PaymentsType = CashFlow.Communication.Enums.PaymentsType.CreditCArd
        };*/


    }
}
