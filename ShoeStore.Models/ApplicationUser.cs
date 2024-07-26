using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
    }
}
