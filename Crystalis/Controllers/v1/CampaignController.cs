using Crystalis.DTO.Campaign;
using Crystalis.Models;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;
using Sieve.Models;

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

    [HttpGet("index")]
    public async Task<IActionResult> Index([FromQuery] SieveModel sieveModel)
    {
        var campaigns = await _campaignService.Get(sieveModel, HttpContext.User);
        return Ok(campaigns);
    }
    [AutoValidation]
    [HttpGet]
    public IActionResult Get([FromQuery] GetCampaignDto getCampaignDto)
    {
        var campaigns = _campaignService.Get(getCampaignDto.Uuid);
        return Ok(campaigns);
    }

    // [Authorize(Policy = "PlayerAccess")]


    [AutoValidation]
    [HttpPatch("update")]
    public  IActionResult Update([FromBody] UpdateCampaignDto campaignDto)
    {
        var campaign = _campaignService.Update(campaignDto);
        return Ok(campaign);
    }
    [AutoValidation]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCampaignDto campaignDto)
    {
        var campaign = await _campaignService.Create(campaignDto, HttpContext.User);
        return Ok(campaign);
    }

    [AutoValidation]
    [HttpDelete]
    public IActionResult Delete([FromBody] GetCampaignDto getCampaignDto)
    {
        _campaignService.Delete(getCampaignDto.Uuid);
        return Ok();
    }

    // [Authorize(Policy = "PlayerAccess")]


    [AutoValidation]
    [HttpPost("join")]
    public async Task<IActionResult> Join([FromBody] GetCampaignDto campaignDto)
    {
        var campaign = await _campaignService.Join(campaignDto, HttpContext.User);
        return Ok(campaign);
    }

    [AutoValidation]
    [HttpPost("leave")]
    public async Task<IActionResult> Leave([FromBody] GetCampaignDto campaignDto)
    {
        var campaign = await _campaignService.Leave(campaignDto, HttpContext.User);
        return Ok(campaign);
    }
}