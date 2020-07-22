using System.ComponentModel.DataAnnotations;

namespace LogSubmissionPortal.Models
{
    public class Log
    {
        [Display(Name = "Full Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        [Required]
        public string EmailAddress { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Subject { get; set; }

        [StringLength(500, MinimumLength = 3)]
        [Required]
        public string Message { get; set; }
    }
}