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
    [Authorize(Roles = "GameMaster")]
    [HttpPost(Name = "create")]
    public IActionResult Create([FromBody] CreateCampaignDto campaignDto)
    {
        var campaign = _campaignService.Add(campaignDto);
        return Ok(campaign);
    }
}