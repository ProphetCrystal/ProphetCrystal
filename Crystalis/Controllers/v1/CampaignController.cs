using Crystalis.DTO.Campaign;
using Crystalis.Models;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crystalis.Controllers.v1;

[ApiController]
[Route("api/campaign")]
public class CampaignController : ControllerBase
{
    private readonly ILogger<CampaignController> _logger;
    private readonly ICampaignService _campaignService;

    public CampaignController(ILogger<CampaignController> logger, ICampaignService campaignService)
    {
        _logger = logger;
        _campaignService = campaignService;
    }
    // [Authorize(Policy = "PlayerAccess")]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCampaignDto campaignDto)
    {
        var campaign = await _campaignService.Add(campaignDto, HttpContext.User);
        return Ok(campaign);
    }
    
    // [Authorize(Policy = "PlayerAccess")]
    [HttpPost("join")]
    public async Task<IActionResult> Join([FromBody] JoinCampaignDto campaignDto)
    {
        var campaign = await _campaignService.Join(campaignDto, HttpContext.User);
        return Ok(campaign);
    }
}