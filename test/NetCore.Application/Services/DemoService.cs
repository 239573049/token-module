using NetCore.Application.Contracts.Services;
using Token.Module.Dependencys;

namespace NetCore.Application.Services;

public class DemoService : IDemoService, ISingletonDependency
{
    public Task<string> GetAsync()
    {

        return Task.FromResult("ok");
    }

    public async Task UpdateAsync(Guid id, string data)
    {
        await Task.CompletedTask;
    }
}