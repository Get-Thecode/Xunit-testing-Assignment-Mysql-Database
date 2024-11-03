using ClaimsManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimService _claimService;
        private readonly S3Service _s3Service;

        public ClaimsController(IClaimService claimService, S3Service s3Service)
        {
            _claimService = claimService;
            _s3Service = s3Service;
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

            // Upload file to S3 if present
            if (file != null)
            {
                using var stream = file.OpenReadStream();
                var filePath = await _s3Service.UploadFileAsync(stream, file.FileName);
                claim.FilePath = filePath;  // Save the S3 file URL in the database
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
