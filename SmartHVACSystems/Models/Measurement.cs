using Newtonsoft.Json;
using System.Text;

namespace SmartHVACSystems.Models;

public class DeviceMessage
{
    [JsonProperty("id")]
    public string? Id { get; init; }
    
    [JsonProperty("device")]
    public string? Device { get; init; }

    [JsonProperty("Properties")]
    public Dictionary<string, object>? Properties { get; init; }

    [JsonProperty("SystemProperties")]
    public Dictionary<string, object>? SystemProperties { get; init; }

    [JsonProperty("iothub-name")]
    public string? IoTHubName { get; init; }

    [JsonProperty("Body")]
    public string Body { get; init; }
    
    [JsonProperty("_rid")]
    public string? ResourceId { get; init; }

    [JsonProperty("_self")]
    public string? Self { get; init; }

    [JsonProperty("_etag")]
    public string? ETag { get; init; }

    [JsonProperty("_attachments")]
    public string? Attachments { get; init; }

    [JsonProperty("_ts")]
    public long? Timestamp { get; init; }
}

public class Measurement
{
    public string? Id { get; init; }
    public string? Device { get; init; }
    public bool Anomaly { get; init; }
    public DateTime TimeStamp { get; init ; }
}

public static class MeasurementConverterExtensions
{
    private sealed class MeasurementBody
    {
        public bool Anomaly { get; set; }
    }

    public static Measurement ToDeviceMeasurement(this DeviceMessage deviceMessage)
    {
        byte[] bytes = Convert.FromBase64String(deviceMessage.Body);
        string decodedString = Encoding.UTF8.GetString(bytes);
        // Deserialize the Body property into an anonymous object
        var bodyObj = JsonConvert.DeserializeObject<MeasurementBody>(decodedString);

        
            // Create a new Measurement object
            var measurement = new Measurement
            {
                Id = deviceMessage.Id,
                Device = deviceMessage.Device?.Split('-')[0],
                Anomaly = bodyObj?.Anomaly ?? false,
                TimeStamp = DateTimeOffset.FromUnixTimeSeconds(deviceMessage.Timestamp ?? 0).DateTime.ToLocalTime(),
            };

        return measurement;
    }
}