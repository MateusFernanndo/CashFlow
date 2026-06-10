namespace CashFlow.Domain.Security.Crytography;

public interface IPasswordEncripter
{
    string Encrypt(string password);
    bool Verify(string password, string passwordHash);
}
