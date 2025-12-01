using System.ComponentModel.DataAnnotations;

namespace VianasCodeLab.Model
{
    public class Computer
    {
        [Key]
        public Guid id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must contain between {2} and {1} characters.", MinimumLength = 2)]
        public string? Name { get; set; }

        public bool Status { get; set; }
    }
}
