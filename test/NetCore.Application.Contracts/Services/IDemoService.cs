namespace NetCore.Application.Contracts.Services;

public interface IDemoService
{
    Task<string> GetAsync();

    Task UpdateAsync(Guid id,string data);
}