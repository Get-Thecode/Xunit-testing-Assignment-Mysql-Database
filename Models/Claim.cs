namespace ClaimsManagementApi;
using System.ComponentModel.DataAnnotations;

public class Claim
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Patient name is required.")]
    public string PatientName { get; set; }

    [Required(ErrorMessage = "Claim amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Claim amount must be greater than zero.")]
    public decimal ClaimAmount { get; set; }

    [Required(ErrorMessage = "Date of claim is required.")]
    public DateTime DateOfClaim { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pending";

    public string? FilePath { get; set; }
}
