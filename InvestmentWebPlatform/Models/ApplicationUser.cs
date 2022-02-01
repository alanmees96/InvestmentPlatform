using Microsoft.AspNetCore.Identity;

namespace InvestmentWebPlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CPF { get; set; }
        public string Name { get; set; }
    }
}