using Microsoft.EntityFrameworkCore;

namespace MyToDo.API.DataModel
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options) 
        { 
            
        }

        public DbSet<AccountInfo> AccountInfos { get; set; }
    }
}
