using System.ComponentModel.DataAnnotations;

namespace FlightManagement.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
