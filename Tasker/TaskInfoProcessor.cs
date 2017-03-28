using System;
using System.Threading;
using System.Threading.Tasks;
using Tasker.Data;
using Tasker.Objects;
using TaskStatus = Tasker.Objects.TaskStatus;

namespace Tasker
{
    public class TaskInfoProcessor
    {
        private readonly TaskInfoRepository _repository;
        private readonly TaskFactory _taskFactory;
        private readonly TimeSpan _queryInterval;

        public TaskInfoProcessor(TaskInfoRepository repository, TaskFactory taskFactory, TimeSpan queryInterval)
        {
            _repository = repository;
            _taskFactory = taskFactory;
            _queryInterval = queryInterval;
        }

        public async Task Process(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_queryInterval, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    var taskInfos = _repository.GetNew(DateTime.UtcNow);
                    foreach (var taskInfo in taskInfos)
                    {
                        var task = _taskFactory.CreateTask(taskInfo);
                        ExecuteTask(task, taskInfo, cancellationToken);
                    }
                }
            }
        }

        private void ExecuteTask(ITask task, TaskInfo taskInfo, CancellationToken cancellationToken)
        {
            Task.Run(async () => await task.Execute(cancellationToken))
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        taskInfo.Status = TaskStatus.Failed;
                    }
                    else if (t.IsCanceled)
                    {
                        taskInfo.Status = TaskStatus.New;
                    }
                    else
                    {
                        taskInfo.Status = TaskStatus.Completed;
                    }
                    _repository.Update(taskInfo);
                });
        }
    }
}
