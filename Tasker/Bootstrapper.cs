using System;
using Newtonsoft.Json;
using Tasker.Data;
using Tasker.Objects;
using Tasker.Objects.Tasks.Settings;

namespace Tasker
{
    public class Bootstrapper
    {
        private readonly TaskInfoRepository _repository;

        public Bootstrapper(TaskInfoRepository repository)
        {
            _repository = repository;
        }

        public void Init()
        {
            InitDataBase();
        }

        private void InitDataBase()
        {
            _repository.Configure(new[]
            {
                new TaskInfo
                {
                    Id = 1,
                    Description = "Создает пустой файл",
                    StartDateTime = DateTime.UtcNow.AddHours(-2),
                    Parameters = JsonConvert.SerializeObject(
                        new CreateFileSettings
                        {
                            Directory = "D:\\Temp",
                            FileName = "Test"
                        }),
                    Status = TaskStatus.New,
                    Type = TaskType.CreateFile
                },
                new TaskInfo
                {
                    Id = 2,
                    Description = "Отправляет почту",
                    StartDateTime = DateTime.UtcNow.AddHours(-1),
                    Parameters = JsonConvert.SerializeObject(
                        new SendMailSettings
                        {
                            Message = new MessageInfo
                            {
                                From = "",
                                To = "",
                                Subject = "",
                                Body = ""
                            },
                            SmtpSettings = new SmtpSettings
                            {
                                SmtpServer = "",
                                Login = "",
                                Password = "",
                                Port = 25
                            }
                        }),
                    Status = TaskStatus.New,
                    Type = TaskType.SendMail
                }
            });
        }
    }
}
