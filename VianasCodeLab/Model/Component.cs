using System.ComponentModel.DataAnnotations;

namespace VianasCodeLab.Model;

public class Component
{
    [Key]
    public Guid id { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    [StringLength(50, ErrorMessage = "The field {0} must contain between {2} and {1} characters.", MinimumLength = 2)]
    public string? Description { get; set; }

    public int Amount { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    [StringLength(50, ErrorMessage = "The field {0} must contain between {2} and {1} characters.", MinimumLength = 2)]
    public string? Mark { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool Status { get; set; }
}
