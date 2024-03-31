using SmartHVACSystems.Models;

namespace SmartHVACSystems.Services;

public interface ICosmosDbService
{
    public Task<IEnumerable<Measurement>> GetMultipleAsync();
}
