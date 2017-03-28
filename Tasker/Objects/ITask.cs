using System.Threading;
using System.Threading.Tasks;

namespace Tasker.Objects
{
    public interface ITask
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
