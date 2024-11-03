namespace ClaimsManagementApi.Services
{
    public interface IClaimService
    {
        Task<Claim> CreateClaimAsync(Claim claim);
        Task<IEnumerable<Claim>> GetAllClaimsAsync();
        Task<Claim> GetClaimByIdAsync(int id);
        Task<Claim> UpdateClaimAsync(int id, Claim claim);
        Task<bool> DeleteClaimAsync(int id);
    }
}
