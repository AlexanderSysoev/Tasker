using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasker.Objects
{
    public class TaskInfo
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        [Index("StartDateTimeAndStatus", 1, IsUnique = false)]
        public DateTime StartDateTime { get; set; }

        public string Parameters { get; set; }

        public TaskType Type { get; set; }

        [Index("StartDateTimeAndStatus", 2, IsUnique = false)]
        public TaskStatus Status { get; set; }
    }
}
