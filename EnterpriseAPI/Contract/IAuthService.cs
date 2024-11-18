using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace EnterpriseAPI.Contract
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(LoginUser loginUser);
        Task<bool> Login(LoginUser loginUser);
        string GenerateToken(LoginUser loginUser);
    }
}
