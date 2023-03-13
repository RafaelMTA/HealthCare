using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Serialization;

namespace HealthCare.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(100, ErrorMessage = "First Name should have a maximum of 100 characters")]
    public string FirstName { get; set; } = String.Empty;
    [Required]
    [MaxLength(100, ErrorMessage = "First Name should have a maximum of 100 characters")]
    public string LastName { get; set; } = String.Empty;
    public string FullName { get
        {
            return FirstName + " " + LastName;
        }
    }
}

