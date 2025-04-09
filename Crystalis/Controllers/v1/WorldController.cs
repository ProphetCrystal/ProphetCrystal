using Crystalis.DTO.Campaign;
using Crystalis.DTO.World;
using Crystalis.Models;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;
using Sieve.Models;

namespace Crystalis.Controllers.v1;

[ApiController]
[Route("api/world")]
public class WorldController : ControllerBase
{
    private readonly ILogger<WorldController> _logger;
    private readonly IWorldService _worldService;

    public WorldController(ILogger<WorldController> logger, IWorldService worldService)
    {
        _logger = logger;
        _worldService = worldService;
    }

    [HttpGet("index")]
    public async Task<IActionResult> Index([FromQuery] SieveModel sieveModel)
    {
        var campaigns = await _worldService.Get(sieveModel, HttpContext.User);
        return Ok(campaigns);
    }
    [AutoValidation]
    [HttpGet]
    public IActionResult Get([FromQuery] GetWorldDto getWorldDto)
    {
        var campaigns = _worldService.Get(getWorldDto.Uuid);
        return Ok(campaigns);
    }

    // [Authorize(Policy = "PlayerAccess")]


    [AutoValidation]
    [HttpPatch("update")]
    public  IActionResult Update([FromBody] UpdateWorldDto updateWorldDto)
    {
        var campaign = _worldService.Update(updateWorldDto);
        return Ok(campaign);
    }
    
    [AutoValidation]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateWorldDto createWorldDto)
    {
        var campaign = await _worldService.Create(createWorldDto, HttpContext.User);
        return Ok(campaign);
    }

    [AutoValidation]
    [HttpDelete]
    public IActionResult Delete([FromBody] GetWorldDto getWorldDto)
    {
        _worldService.Delete(getWorldDto.Uuid);
        return Ok();
    }
}