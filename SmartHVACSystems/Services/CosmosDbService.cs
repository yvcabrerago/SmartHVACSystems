using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using SmartHVACSystems.Models;

namespace SmartHVACSystems.Services;

public class CosmosDbService : ICosmosDbService
{
    private Container _container;

    public CosmosDbService(
        CosmosClient cosmosDbClient,
        string databaseName,
        string containerName)
    {
        _container = cosmosDbClient.GetContainer(databaseName, containerName);
    }

    public async Task<IEnumerable<Measurement>> GetMultipleAsync()
    {
        var query = _container.GetItemQueryIterator<DeviceMessage>(
        new QueryDefinition("SELECT TOP 10 * FROM c ORDER BY c.TimestampField DESC"));

        var results = new List<DeviceMessage>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        var res =  results.Select(devMsg => devMsg.ToDeviceMeasurement());
        return res;
    }

    public Task<IEnumerable<Measurement>> GetMultipleAsync(string query)
    {
        throw new NotImplementedException();
    }
}
