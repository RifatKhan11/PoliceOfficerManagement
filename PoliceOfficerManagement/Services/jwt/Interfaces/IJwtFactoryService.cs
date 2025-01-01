namespace PoliceOfficerManagement.Services.jwt.Interfaces
{
    public interface IJwtFactoryService
    {
        Task<String> GenerateToken(string userName, string id, IList<string> roles);
    }
}
