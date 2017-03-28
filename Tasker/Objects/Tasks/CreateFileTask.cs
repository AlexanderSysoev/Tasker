using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tasker.Objects.Tasks.Settings;

namespace Tasker.Objects.Tasks
{
    public class CreateFileTask : ITask
    {
        private readonly CreateFileSettings _settings;

        public CreateFileTask(string settingsJson)
        {
            _settings = JsonConvert.DeserializeObject<CreateFileSettings>(settingsJson);
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                File.Create(Path.Combine(_settings.Directory, _settings.FileName));
            }
        }
    }
}
