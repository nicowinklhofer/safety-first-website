using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPL_Diplom_Winki_Trippi_Sabi.Models;

namespace SafetyFirstWebsite.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

    }
}
