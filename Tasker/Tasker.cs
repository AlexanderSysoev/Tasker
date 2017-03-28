using System;
using System.Threading;
using System.Threading.Tasks;
using Tasker.Data;

namespace Tasker
{
    public class Tasker
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        public Tasker()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            var repository = new TaskInfoRepository();
            var bootstrapper = new Bootstrapper(repository);
            var taskInfoProcessor = new TaskInfoProcessor(repository, new TaskFactory(), TimeSpan.FromSeconds(5));

            Task.Run(async () =>
            {
                bootstrapper.Init();
                await taskInfoProcessor.Process(_cancellationTokenSource.Token);
            });
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
