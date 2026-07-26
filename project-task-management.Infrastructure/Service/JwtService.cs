using Microsoft.IdentityModel.Tokens;
using project_task_management.Application.Interface.Repository;
using project_task_management.Application.Interface.Service;
using project_task_management.Application.ResultHandler;
using project_task_management.Domain.Entities.Identity;
using project_task_management.Domain.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_task_management.Infrastructure.Service
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;

        public JwtService(JwtSettings jwtSettings, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings;
            _userRepository = userRepository;
        }

        public async Task<AuthJwtResult> GetJWTToken(User user)
        {

            var (jwtToken, accessToken) = await GenerateJWTToken(user);

            var response = new AuthJwtResult();
            response.AccessToken = accessToken;
            return response;

        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        public async Task<List<Claim>> GetClaims(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.Id), user.Id),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
            };

            var userClaims = await _userRepository.GetUserManager().GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        public JwtSecurityToken ReadAccessJWTToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }
        public bool ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
