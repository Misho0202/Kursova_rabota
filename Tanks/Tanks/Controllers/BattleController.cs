using Microsoft.AspNetCore.Mvc;
using Tanks.Application.Interfaces;
using Tanks.Domain.DTOs;

namespace Tanks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BattleController : ControllerBase
{
    private readonly IBattleService _battleService;

    public BattleController(IBattleService battleService)
    {
        _battleService = battleService;
    }

    [HttpPost("simulate")]
    public async Task<IActionResult> SimulateBattle([FromBody] BattleDto battleDto)
    {
        var battle = await _battleService.SimulateBattleAsync(battleDto.Tank1Id, battleDto.Tank2Id);
        return Ok(battle);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetBattleHistory()
    {
        var history = await _battleService.GetBattleHistoryAsync();
        return Ok(history);
    }
}