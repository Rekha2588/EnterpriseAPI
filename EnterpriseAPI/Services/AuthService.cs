using EnterpriseAPI.Contract;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnterpriseAPI.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            this._userManager = userManager;
            this._config = config;
        }

        public async Task<IdentityResult> Register(LoginUser loginUser)
        {
            var identityUser = new IdentityUser
            {
                UserName = loginUser.UserName,
                Email = loginUser.UserName
            };

            var result = await _userManager.CreateAsync(identityUser, loginUser.Password);
            return result;
        }

        public async Task<bool> Login(LoginUser loginUser)
        {           

            var identityUser = await _userManager.FindByEmailAsync(loginUser.UserName);
            if(identityUser != null)
            {
                return await _userManager.CheckPasswordAsync(identityUser,loginUser.Password);
            }
            return false;
        }

        public string GenerateToken(LoginUser loginUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginUser.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("JWT:Issuer").Value,
                audience: _config.GetSection("JWT:Audience").Value,
                signingCredentials: creds
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
