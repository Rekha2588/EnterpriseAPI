using EnterpriseAPI.Models;

namespace EnterpriseAPI.Contract
{
    public interface IAuthService
    {
        Task<bool> Register(LoginUser loginUser);
        Task<bool> Login(LoginUser loginUser);
        string GenerateToken(LoginUser loginUser);
    }
}
