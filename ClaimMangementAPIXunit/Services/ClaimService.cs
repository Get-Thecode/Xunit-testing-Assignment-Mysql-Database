using ClaimsManagementApi.Repositories;

namespace ClaimsManagementApi.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _repository;

        public ClaimService(IClaimRepository repository)
        {
            _repository = repository;
        }

        public async Task<Claim> CreateClaimAsync(Claim claim)
        {
            if (string.IsNullOrWhiteSpace(claim.PatientName) ||
                claim.ClaimAmount <= 0 ||
                claim.DateOfClaim > DateTime.Now)
            {
                throw new ArgumentException("Invalid claim data.");
            }
            return await _repository.AddClaimAsync(claim);
        }

        public async Task<IEnumerable<Claim>> GetAllClaimsAsync()
        {
            return await _repository.GetAllClaimsAsync();
        }

        public async Task<Claim> GetClaimByIdAsync(int id)
        {
            return await _repository.GetClaimByIdAsync(id);
        }

        public async Task<Claim> UpdateClaimAsync(int id, Claim updatedClaim)
        {
            var existingClaim = await _repository.GetClaimByIdAsync(id);
            if (existingClaim == null)
                throw new KeyNotFoundException("Claim not found.");

            existingClaim.PatientName = updatedClaim.PatientName;
            existingClaim.ClaimAmount = updatedClaim.ClaimAmount;
            existingClaim.DateOfClaim = updatedClaim.DateOfClaim;
            existingClaim.Status = updatedClaim.Status;
            existingClaim.FilePath = updatedClaim.FilePath;

            return await _repository.UpdateClaimAsync(existingClaim);
        }

        public async Task<bool> DeleteClaimAsync(int id)
        {
            return await _repository.DeleteClaimAsync(id);
        }
    }
}
