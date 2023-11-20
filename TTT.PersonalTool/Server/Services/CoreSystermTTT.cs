using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TTT.PersonalTool.Contracts.IRepositories;
using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Server.Services.IServices;

namespace TTT.PersonalTool.Server.Services
{
    public class CoreSystermTTT : ICoreSystermTTT
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        public CoreSystermTTT(IConfiguration configuration, ILogger logger, IUserRepository userRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _userRepository = userRepository;
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

        public Task<User> GetRequestUser()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByJWT(string jwtToken)
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
                return await _userRepository.GetByIdAsync(Convert.ToInt32(userId)) ?? new User();
            }
            return new User();
        }
    }
}
