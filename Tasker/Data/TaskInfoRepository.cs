using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Tasker.Objects;

namespace Tasker.Data
{
    public class TaskInfoRepository
    {
        public IEnumerable<TaskInfo> GetNew(DateTime now)
        {
            return WithContext(context =>
            {
                using (var transaction = context.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                {
                    try
                    {
                        var taskInfos = context.TaskInfoItems.Where(ti =>
                            ti.Status == TaskStatus.New && ti.StartDateTime <= now);

                        foreach (var taskInfo in taskInfos)
                        {
                            taskInfo.Status = TaskStatus.Processing;
                        }

                        var result = taskInfos.ToList();

                        context.SaveChanges();
                        transaction.Commit();

                        return result;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            });
        }

        public void Update(TaskInfo taskInfo)
        {
            WithContext(context =>
            {
                context.TaskInfoItems.Attach(taskInfo);
                context.Entry(taskInfo).State = EntityState.Modified;
                context.SaveChanges();
            });
        }

        public void Configure(IEnumerable<TaskInfo> taskInfos)
        {
            WithContext(context =>
            {
                if (!context.Database.Exists())
                {
                    context.Database.Create();
                    context.TaskInfoItems.AddRange(taskInfos);
                    context.SaveChanges();
                }
            });
        }

        private static T WithContext<T>(Func<TaskInfoDbContext, T> func)
        {
            using (var context = new TaskInfoDbContext())
            {
                return func(context);
            }
        }

        private static void WithContext(Action<TaskInfoDbContext> action)
        {
            using (var context = new TaskInfoDbContext())
            {
                action(context);
            }
        }
    }
}
