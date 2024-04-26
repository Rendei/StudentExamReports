using Microsoft.AspNetCore.Identity;

namespace UTMNStudentsExamAnalysis.Models
{
    public class ApplicationUser : IdentityUser
    {
        public override string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
