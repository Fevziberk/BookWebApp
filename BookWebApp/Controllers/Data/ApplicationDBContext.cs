using BookWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWebApp.Controllers.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }
        public DbSet<Category> Categories { get; set; }  
    }
}
