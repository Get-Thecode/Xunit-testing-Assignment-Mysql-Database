using ClaimsManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimsController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        // POST /api/claims: Create a new claim
        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromForm] Claim claim, IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate ClaimAmount
            if (claim.ClaimAmount <= 0)
                return BadRequest("ClaimAmount must be greater than zero.");

            // Validate DateOfClaim
            if (claim.DateOfClaim > DateTime.Now)
                return BadRequest("DateOfClaim cannot be in the future.");

            // Handle file upload if present
            if (file != null)
            {
                var filePath = Path.Combine("wwwroot", "Uploads", $"{Guid.NewGuid()}_{file.FileName}");
                Directory.CreateDirectory(Path.Combine("wwwroot", "Uploads")); // Ensure the directory exists

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                // Save the relative path for easier reference
                claim.FilePath = Path.Combine("Uploads", $"{Guid.NewGuid()}_{file.FileName}");
            }



            var createdClaim = await _claimService.CreateClaimAsync(claim);
            return CreatedAtAction(nameof(GetClaimById), new { id = createdClaim.Id }, createdClaim);
        }

        // GET /api/claims: Retrieve a list of all claims
        [HttpGet]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return Ok(claims);
        }

        // GET /api/claims/{id}: Retrieve a specific claim by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
                return NotFound();

            return Ok(claim);
        }

        // PUT /api/claims/{id}: Update an existing claim
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaim(int id, [FromBody] Claim updatedClaim)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate ClaimAmount
            if (updatedClaim.ClaimAmount <= 0)
                return BadRequest("ClaimAmount must be greater than zero.");

            // Validate DateOfClaim
            if (updatedClaim.DateOfClaim > DateTime.Now)
                return BadRequest("DateOfClaim cannot be in the future.");

            var result = await _claimService.UpdateClaimAsync(id, updatedClaim);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE /api/claims/{id}: Delete a claim
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            var success = await _claimService.DeleteClaimAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
