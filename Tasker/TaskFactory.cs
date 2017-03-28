using System;
using Tasker.Objects;
using Tasker.Objects.Tasks;

namespace Tasker
{
    public class TaskFactory
    {
        public ITask CreateTask(TaskInfo taskInfo)
        {
            switch (taskInfo.Type)
            {
                case TaskType.CreateFile:
                {
                    return new CreateFileTask(taskInfo.Parameters);
                }
                case TaskType.SendMail:
                {
                    return new SendMailTask(taskInfo.Parameters);
                }

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
