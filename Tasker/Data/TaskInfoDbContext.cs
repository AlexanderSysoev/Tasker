using System.Data.Entity;
using Tasker.Objects;

namespace Tasker.Data
{
    public class TaskInfoDbContext : DbContext
    {
        public TaskInfoDbContext() : base("TaskInfo")
        {
        }

        public DbSet<TaskInfo> TaskInfoItems { get; set; }
    }
}
