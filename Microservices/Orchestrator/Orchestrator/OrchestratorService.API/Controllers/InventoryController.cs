using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrchestratorService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    public InventoryController()
    {
        
    }

    [HttpGet]
    [Authorize]
    public ActionResult GetInventory(CancellationToken token = default)
    {
        try
        {
            return Ok();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"---> Get Inventory Method error: {ex.Message}");
            
            return BadRequest(ex.Message);
        }
    }
}