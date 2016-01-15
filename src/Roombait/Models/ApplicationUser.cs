using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Roombait.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        [NotMapped]
        public string Name
        {
            get { return FullName ?? UserName ?? Email; }
        }
    }
}
