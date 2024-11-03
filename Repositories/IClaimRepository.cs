namespace ClaimsManagementApi.Repositories
{
    public interface IClaimRepository
    {
        Task<Claim> AddClaimAsync(Claim claim);
        Task<IEnumerable<Claim>> GetAllClaimsAsync();
        Task<Claim> GetClaimByIdAsync(int id);
        Task<Claim> UpdateClaimAsync(Claim claim);
        Task<bool> DeleteClaimAsync(int id);
    }
}
