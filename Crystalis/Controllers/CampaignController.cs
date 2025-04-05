using Crystalis.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crystalis.Controllers;

[ApiController]
[Route("api/campaign")]
public class CampaignController : ControllerBase
{
    private readonly ILogger<CampaignController> _logger;

    public CampaignController(ILogger<CampaignController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "")]
    public IActionResult Get()
    {
        return Ok();
    }
}