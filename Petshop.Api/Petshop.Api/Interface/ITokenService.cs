namespace Petshop.Api.Interface
{
    public interface ITokenService
    {
        Task<object?> EfetuarLogin(string email, string senha);
    }
}
