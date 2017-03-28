using Topshelf;

namespace Tasker
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<Tasker>(s =>
                {
                    s.ConstructUsing(name => new Tasker());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Simple scheduler service");
                x.SetDisplayName("Tasker");
                x.SetServiceName("Tasker");
            });
        }
    }
}
