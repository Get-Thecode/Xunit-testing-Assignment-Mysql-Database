using ClaimsManagementApi.Controllers;
using ClaimsManagementApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ClaimsManagementApi.Tests.Controllers
{
    public class ClaimsControllerTests
    {
            private readonly ClaimsController _controller;
            private readonly Mock<IClaimService> _mockClaimService;

            public ClaimsControllerTests()
            {
                _mockClaimService = new Mock<IClaimService>();
                _controller = new ClaimsController(_mockClaimService.Object);
            }

            // Test for POST /api/claims - CreateClaim
            [Fact]
            public async Task CreateClaim_ShouldReturnCreatedResult_WhenValidClaimIsProvided()
            {
                // Arrange
                var claim = new Claim { PatientName = "John Doe", ClaimAmount = 100.0M, DateOfClaim = DateTime.Now };
                _mockClaimService.Setup(s => s.CreateClaimAsync(It.IsAny<Claim>())).ReturnsAsync(claim);

                // Act
                var result = await _controller.CreateClaim(claim, null);

                // Assert
                var createdResult = Assert.IsType<CreatedAtActionResult>(result);
                Assert.Equal(201, createdResult.StatusCode);
            }

            [Fact]
            public async Task CreateClaim_ShouldReturnBadRequest_WhenInvalidClaimAmount()
            {
                // Arrange
                var claim = new Claim { PatientName = "John Doe", ClaimAmount = 0, DateOfClaim = DateTime.Now };

                // Act
                var result = await _controller.CreateClaim(claim, null);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result);
            }

            [Fact]
            public async Task CreateClaim_ShouldReturnBadRequest_WhenDateOfClaimIsInFuture()
            {
                // Arrange
                var claim = new Claim { PatientName = "Jane Doe", ClaimAmount = 100.0M, DateOfClaim = DateTime.Now.AddDays(1) };

                // Act
                var result = await _controller.CreateClaim(claim, null);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result);
            }

            // Test for GET /api/claims - GetAllClaims
            [Fact]
            public async Task GetAllClaims_ShouldReturnOkResult_WithListOfClaims()
            {
                // Arrange
                var claims = new List<Claim> { new Claim { PatientName = "John Doe", ClaimAmount = 100.0M } };
                _mockClaimService.Setup(s => s.GetAllClaimsAsync()).ReturnsAsync(claims);

                // Act
                var result = await _controller.GetAllClaims();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(200, okResult.StatusCode);
                Assert.Equal(claims, okResult.Value);
            }

            // Test for GET /api/claims/{id} - GetClaimById
            [Fact]
            public async Task GetClaimById_ShouldReturnOkResult_WhenClaimExists()
            {
                // Arrange
                var claim = new Claim { Id = 1, PatientName = "Jane Smith", ClaimAmount = 150.0M };
                _mockClaimService.Setup(s => s.GetClaimByIdAsync(claim.Id)).ReturnsAsync(claim);

                // Act
                var result = await _controller.GetClaimById(claim.Id);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(claim, okResult.Value);
            }

            [Fact]
            public async Task GetClaimById_ShouldReturnNotFound_WhenClaimDoesNotExist()
            {
                // Arrange
                _mockClaimService.Setup(s => s.GetClaimByIdAsync(It.IsAny<int>())).ReturnsAsync((Claim)null);

                // Act
                var result = await _controller.GetClaimById(1);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }

            // Test for PUT /api/claims/{id} - UpdateClaim
            [Fact]
            public async Task UpdateClaim_ShouldReturnOkResult_WhenValidClaimIsProvided()
            {
                // Arrange
                var updatedClaim = new Claim { Id = 1, PatientName = "Tom Brown", ClaimAmount = 200.0M };
                _mockClaimService.Setup(s => s.UpdateClaimAsync(It.IsAny<int>(), It.IsAny<Claim>())).ReturnsAsync(updatedClaim);

                // Act
                var result = await _controller.UpdateClaim(updatedClaim.Id, updatedClaim);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(updatedClaim, okResult.Value);
            }

            [Fact]
            public async Task UpdateClaim_ShouldReturnNotFound_WhenClaimDoesNotExist()
            {
                // Arrange
                _mockClaimService.Setup(s => s.UpdateClaimAsync(It.IsAny<int>(), It.IsAny<Claim>())).ReturnsAsync((Claim)null);

                // Act
                var result = await _controller.UpdateClaim(1, new Claim { Id = 1, PatientName = "Tom Brown" });

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }

            // Test for DELETE /api/claims/{id} - DeleteClaim
            [Fact]
            public async Task DeleteClaim_ShouldReturnNoContent_WhenClaimIsDeleted()
            {
                // Arrange
                _mockClaimService.Setup(s => s.DeleteClaimAsync(It.IsAny<int>())).ReturnsAsync(true);

                // Act
                var result = await _controller.DeleteClaim(1);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async Task DeleteClaim_ShouldReturnNotFound_WhenClaimDoesNotExist()
            {
                // Arrange
                _mockClaimService.Setup(s => s.DeleteClaimAsync(It.IsAny<int>())).ReturnsAsync(false);

                // Act
                var result = await _controller.DeleteClaim(1);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        
    }
}
