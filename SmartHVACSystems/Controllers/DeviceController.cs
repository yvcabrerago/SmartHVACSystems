using Humanizer;
using Microsoft.AspNetCore.Mvc;
using SmartHVACSystems.Services;

namespace SmartHVACSystems.Controllers;
public class DeviceController : Controller
{
    private readonly ICosmosDbService _cosmosDbService;

    public DeviceController(ICosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
    }
    
    public async Task<IActionResult> Index()
    {
        var measurements = await _cosmosDbService.GetMultipleAsync();
        return View(measurements);
    }
}
