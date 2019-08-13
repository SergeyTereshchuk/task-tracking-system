namespace TaskTrackingSystem.WebApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRoleModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Name { get; set; }
    }
}