using TodoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<TodoTask>Tasks{ get; set; }
    }
}