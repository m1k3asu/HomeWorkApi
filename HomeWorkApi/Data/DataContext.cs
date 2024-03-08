using HomeWorkApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectArkansasAde.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbOptions) : base(dbOptions)
        {

        }

        public DbSet<Customers> Customers => Set<Customers>();
    }
       
}
