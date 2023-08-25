using Microsoft.EntityFrameworkCore;
using News.WebUI.Entities.Concrete;

namespace News.WebUI.DataAccess.Concrete
{
    public class Context : DbContext
    {
        public Context( DbContextOptions options) : base(options)
        {
        }
        public DbSet<Content>Contents { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<User> Users { get; set; }
         
    }
}
