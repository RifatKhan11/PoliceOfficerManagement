using Microsoft.IdentityModel.Tokens;

namespace PoliceOfficerManagement.Models
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }

        public String Subject { get; set; }

        public String Audience { get; set; }

        public DateTime Expiration => IssuedAt.Add(ValidFor);

        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;

        public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(365);

        public Func<Task<String>> JtiGenerator =>
        () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SigningCredentials { get; set; }
    }
}
