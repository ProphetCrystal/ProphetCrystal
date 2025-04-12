using Crystalis.DTO.World;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;
using Sieve.Models;

namespace Crystalis.Controllers.v1;

[ApiController]
[Route("api/world")]
public class WorldController : ControllerBase
{
    private readonly IWorldService _worldService;

    public WorldController(IWorldService worldService)
    {
        _worldService = worldService;
    }

    [HttpGet("index")]
    [ProducesResponseType<List<WorldDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Index([FromQuery] SieveModel sieveModel)
    {
        List<WorldDto> campaigns = await _worldService.Get(sieveModel, HttpContext.User);
        return Ok(campaigns);
    }

    [AutoValidation]
    [HttpGet]
    [ProducesResponseType<WorldDto>(StatusCodes.Status200OK)]
    public IActionResult Get([FromQuery] GetWorldDto getWorldDto)
    {
        WorldDto campaigns = _worldService.Get(getWorldDto.Uuid);
        return Ok(campaigns);
    }

    // [Authorize(Policy = "PlayerAccess")]


    [AutoValidation]
    [HttpPatch("update")]
    [ProducesResponseType<WorldDto>(StatusCodes.Status200OK)]
    public IActionResult Update([FromBody] UpdateWorldDto updateWorldDto)
    {
        WorldDto campaign = _worldService.Update(updateWorldDto);
        return Ok(campaign);
    }

    [AutoValidation]
    [HttpPost("create")]
    [ProducesResponseType<WorldDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateWorldDto createWorldDto)
    {
        WorldDto campaign = await _worldService.Create(createWorldDto, HttpContext.User);
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