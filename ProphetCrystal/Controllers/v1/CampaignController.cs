using Microsoft.AspNetCore.Mvc;
using ProphetCrystal.DTO.Campaign;
using ProphetCrystal.Services.Interfaces;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;
using Sieve.Models;

namespace ProphetCrystal.Controllers.v1;

[ApiController]
[Route("api/campaign")]
public class CampaignController : ControllerBase
{
    private readonly ICampaignService _campaignService;

    public CampaignController(ICampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    [HttpGet("index")]
    [ProducesResponseType<List<CampaignDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Index([FromQuery] SieveModel sieveModel)
    {
        List<CampaignDto> campaigns = await _campaignService.Get(sieveModel, HttpContext.User);
        return Ok(campaigns);
    }

    [AutoValidation]
    [HttpGet]
    [ProducesResponseType<CampaignDto>(StatusCodes.Status200OK)]
    public IActionResult Get([FromQuery] GetCampaignDto getCampaignDto)
    {
        CampaignDto campaigns = _campaignService.Get(getCampaignDto.Uuid);
        return Ok(campaigns);
    }

    // [Authorize(Policy = "PlayerAccess")]


    [AutoValidation]
    [HttpPatch("update")]
    [ProducesResponseType<CampaignDto>(StatusCodes.Status200OK)]
    public IActionResult Update([FromBody] UpdateCampaignDto campaignDto)
    {
        CampaignDto campaign = _campaignService.Update(campaignDto);
        return Ok(campaign);
    }

    [AutoValidation]
    [HttpPost("create")]
    [ProducesResponseType<CampaignDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCampaignDto campaignDto)
    {
        CampaignDto campaign = await _campaignService.Create(campaignDto, HttpContext.User);
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
    [ProducesResponseType<CampaignDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Join([FromBody] GetCampaignDto campaignDto)
    {
        CampaignDto campaign = await _campaignService.Join(campaignDto, HttpContext.User);
        return Ok(campaign);
    }

    [AutoValidation]
    [HttpPost("leave")]
    [ProducesResponseType<CampaignDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Leave([FromBody] GetCampaignDto campaignDto)
    {
        bool campaign = await _campaignService.Leave(campaignDto, HttpContext.User);
        return Ok(campaign);
    }
}