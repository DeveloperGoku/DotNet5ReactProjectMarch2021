using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
//below line of code,  Activities refer to the name for the table in the database. and will match to the properties of the Activity class in the domain
        public DbSet<Activity> Activities {get;set;}   
    }
}