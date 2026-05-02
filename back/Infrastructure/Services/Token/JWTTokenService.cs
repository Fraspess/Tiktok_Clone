using Application.Dtos.Token;
using Application.Interfaces;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Token
{
    internal class JWTTokenService(IConfiguration configuration, UserManager<UserEntity> userManager) : IJWTTokenService
    {
        private readonly string _key = configuration["Jwt:Key"]
                                       ?? throw new InvalidOperationException("Не настроєний Jwt:key, осечка");

        private readonly string _issuer = configuration["Jwt:Issuer"]
                                          ?? throw new InvalidOperationException("Не настроєний Jwt:Issuer, осечка");

        private readonly string _audience = configuration["Jwt:Audience"]
                                            ?? throw new InvalidOperationException(
                                                "Не настроєний Jwt:Audience, осечка");

        private readonly int _accessTokenExpiry = int.Parse(configuration["Jwt:AccessTokenExpiryMinutes"]
                                                            ?? throw new InvalidOperationException(
                                                                "Не настроєний Jwt:AccessTokenExpiryMinutes, осечка"));

        private readonly int _refreshTokenExpiry = int.Parse(configuration["Jwt:RefreshTokenExpiryDays"]
                                                             ?? throw new InvalidOperationException(
                                                                 "Не настроєний Jwt:RefreshTokenExpiryDays, осечка"));

        public async Task<TokenResponseDTO> GenerateTokensAsync(UserEntity user)
        {
            return new TokenResponseDTO
            {
                AccessToken = await CreateAccessTokenAsync(user),
                RefreshToken = await CreateRefreshTokenAsync(user)
            };
        }

        public async Task<TokenResponseDTO> RefreshTokensAsync(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_key
                    )),
            };

            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out _);
            }
            catch (Exception)
            {
                throw new UnauthorizedException("Не валідний refresh токен");
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? throw new UnauthorizedException("Не валідний refresh токен");

            var tokenVersion = principal.FindFirst("Version")?.Value
                               ?? throw new UnauthorizedException("Не валідний refresh токен");

            var user = userManager.Users.FirstOrDefault(u => u.Id.ToString() == userId)
                       ?? throw new UnauthorizedException("Користувача не знайдено");

            if (user.RefreshTokenVersion != int.Parse(tokenVersion))
            {
                throw new UnauthorizedException("Не валідний refresh токен");
            }

            return await GenerateTokensAsync(user);
        }

        private async Task<string> CreateAccessTokenAsync(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("email", user.Email ?? ""),
                new Claim("username", user.UserName ?? ""),
                new Claim("image", user.Avatar ?? ""),
            };

            foreach (var role in await userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim("role", role));
            }

            var signingCredentials = GetSigningCredentials();

            var accessToken = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_accessTokenExpiry),
                signingCredentials: signingCredentials
            );

            string accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return accessTokenString;
        }


        private async Task<string> CreateRefreshTokenAsync(UserEntity user)
        {
            var claims = new List<Claim>()
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("Version", user.RefreshTokenVersion.ToString()),
            };

            var signingCredentials = GetSigningCredentials();

            var refreshToken = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_refreshTokenExpiry)),
                signingCredentials: signingCredentials
            );

            string refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            return refreshTokenString;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(_key);
            var signingInKey = new SymmetricSecurityKey(keyBytes);
            return new SigningCredentials(signingInKey, SecurityAlgorithms.HmacSha256);
        }
    }
}