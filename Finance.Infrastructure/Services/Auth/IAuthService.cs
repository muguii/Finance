namespace Finance.Infrastructure.Services.Auth
{
    public interface IAuthService
    {
        string ComputeSha256Hash(string password);
    }
}
