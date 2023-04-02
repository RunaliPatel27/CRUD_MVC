using Microsoft.EntityFrameworkCore;
using Sample_CRUD.Models;

namespace Sample_CRUD.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<BookModel> Books {get; set; }
    }
}
