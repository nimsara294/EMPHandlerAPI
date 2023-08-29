using Microsoft.EntityFrameworkCore;

namespace EMPHandlerAPI.Models
{
    public class DepartmentContext : DbContext
    {
        public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options)
        {
            
        }
        public DbSet<Department> Departments { get; set; }
    }
}
