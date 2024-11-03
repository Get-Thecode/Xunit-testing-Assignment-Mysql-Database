using ClaimsManagementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimsManagementApi.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext _context;

        public ClaimRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Claim> AddClaimAsync(Claim claim)
        {
            await _context.Claims.AddAsync(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<IEnumerable<Claim>> GetAllClaimsAsync()
        {
            return await _context.Claims.ToListAsync();
        }

        public async Task<Claim> GetClaimByIdAsync(int id)
        {
            return await _context.Claims.FindAsync(id);
        }

        public async Task<Claim> UpdateClaimAsync(Claim claim)
        {
            _context.Entry(claim).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<bool> DeleteClaimAsync(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
                return false;

            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
