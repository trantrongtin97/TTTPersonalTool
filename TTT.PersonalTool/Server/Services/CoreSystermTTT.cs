using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Server.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace TTT.PersonalTool.Server.Services
{
    /// <summary>
    /// Class core to handle user request and token
    /// </summary>
    public class CoreSystermTTT : ICoreSystermTTT
    {

        private readonly IConfiguration _configuration;
        public CoreSystermTTT(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> GenerateJwtToken(User user)
        {
            string secretKey = _configuration["JWTSettings:SecretKey"] ?? "";
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claimEmail = new Claim(ClaimTypes.Email, user.Username);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
            var claimRole = new Claim(ClaimTypes.Role, user.Role == null ? "" : user.Role);

            var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier, claimRole }, "serverAuth");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        public async Task<int> GetIDRequestUser(HttpContext httpContext)
        {
            var rawToken = await httpContext.GetTokenAsync("access_token");
            //var includeBearer = Request.Headers["Authorization"];
            return await GetUserIDByJWT(rawToken);
        }

        public async Task<int> GetUserIDByJWT(string jwtToken)
        {
            string secretKey = _configuration["JWTSettings:SecretKey"] ?? "";
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = (JwtSecurityToken)securityToken;

            if (jwtSecurityToken != null
                && jwtSecurityToken.ValidTo > DateTime.Now
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Convert.ToInt32(userId);
            }
            return 0;
        }
    }
}
