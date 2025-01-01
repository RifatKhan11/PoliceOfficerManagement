using Microsoft.Extensions.Options;
using PoliceOfficerManagement.Models;
using PoliceOfficerManagement.Services.jwt.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PoliceOfficerManagement.Services.jwt
{
    public class JwtFactoryService : IJwtFactoryService
    {
        public readonly JwtIssuerOptions _jwtIssuerOption;

        public JwtFactoryService(IOptions<JwtIssuerOptions> jwtIssuerOption)
        {
            _jwtIssuerOption = jwtIssuerOption.Value;
        }

        public async Task<string> GenerateToken(string userName, string id, IList<string> roles)
        {

            #region new jwt
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Name, userName));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOption.JtiGenerator()));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOption.IssuedAt).ToString(), ClaimValueTypes.Integer64));
            foreach (var item in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var jwt = new JwtSecurityToken(
                issuer: _jwtIssuerOption.Issuer,
                audience: _jwtIssuerOption.Audience,
                claims: Claims.ToArray(),
                notBefore: _jwtIssuerOption.NotBefore,
                expires: _jwtIssuerOption.Expiration,
                signingCredentials: _jwtIssuerOption.SigningCredentials
            );
            #endregion


            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);
    }
}
