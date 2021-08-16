using System.Threading;
using System.Threading.Tasks;

public interface ITypedConsumer<in T>
{
    public Task ConsumeAsync(T message, CancellationToken cancellationToken);
}